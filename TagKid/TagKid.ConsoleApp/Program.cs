using System.Collections.Generic;
using HtmlAgilityPack;
using StackExchange.StacMan;
using StackExchange.StacMan.Tags;
using Stacky;
using System;
using System.IO;
using System.Linq;
using Taga.Core.IoC;
using Taga.Core.Repository;
using Taga.Core.Repository.Sql;
using TagKid.Lib.Exceptions;
using TagKid.Lib.PetaPoco;
using TagKid.Lib.PetaPoco.Repository;
using TagKid.Lib.PetaPoco.Repository.Sql;
using TagKid.Lib.Repositories;
using TagKid.Lib.Repositories.Impl;
using IMapper = TagKid.Lib.PetaPoco.IMapper;
using Tag = TagKid.Lib.Models.Entities.Tag;

namespace TagKid.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var prov = ServiceProvider.Provider;
                prov.Register<ISqlRepository, PetaPocoSqlRepository>();
                prov.Register<IUnitOfWork, PetaPocoUnitOfWork>();
                prov.Register<ISqlBuilder, PetaPocoSqlBuilder>();
                prov.Register<IPostRepository, PostRepository>();
                prov.Register<ILoginRepository, LoginRepository>();
                prov.Register<ITagRepository, TagRepository>();
                prov.Register<ICommentRepository, CommentRepository>();
                prov.Register<INotificationRepository, NotificationRepository>();
                prov.Register<IPrivateMessageRepository, PrivateMessageRepository>();
                prov.Register<IMapper, TagKidMapper>(new TagKidMapper());

                StacManClient c = new StacManClient("TrhOnlvpT7bZ*Jj5NttVVA((");

                //c.Questions.GetAll(filter:)

                //HashSet<String> existingTags;
                //using (prov.GetOrCreate<IUnitOfWork>())
                //{
                //    var repo = prov.GetOrCreate<ITagRepository>();
                //    var tt = repo.GetAll();
                //    existingTags = new HashSet<String>(tt.Select(t => t.Name));
                //}

                int pageSize = 100;
                StackExchange.StacMan.Tag[] arr;

                var tags = new List<Tag>();

                int page = 1;
                int retry = 10;
                do
                {
                    arr = null;
                    try
                    {
                        arr = c.Tags.GetAll("stackoverflow", page: page++, pagesize: pageSize, sort: Sort.Name).Result.Data.Items;
                        if (arr == null)
                            break;
                        
                        foreach (var tag in arr)
                        {
                            tags.Add(new Tag
                            {
                                Name = tag.Name,
                                Hint = tag.Name,
                                Description = tag.Name
                            });
                        }
                        retry = 10;

                        if (tags.Count % 500 == 0)
                            Console.WriteLine(tags.Count);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("FATAL: " + ex.GetMessage());
                        if (retry-- == 0)
                            break;
                    }
                } while (arr != null && arr.Length == pageSize);

                Console.WriteLine(tags.Count);
                using (var uow = prov.GetOrCreate<IUnitOfWork>())
                {
                    var repo = prov.GetOrCreate<ITagRepository>();
                    foreach (var tag in tags)
                    {
                        try
                        {
                            repo.Save(tag);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(tag.Name + ": " + ex.GetMessage());
                        }
                    }
                    uow.Save();

                }

                //DownloadData();


                Console.WriteLine("OK!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }

        private static void DownloadData()
        {
            var prov = ServiceProvider.Provider;
            prov.Register<ISqlRepository, PetaPocoSqlRepository>();
            prov.Register<IUnitOfWork, PetaPocoUnitOfWork>();
            prov.Register<ISqlBuilder, PetaPocoSqlBuilder>();
            prov.Register<IPostRepository, PostRepository>();
            prov.Register<ILoginRepository, LoginRepository>();
            prov.Register<ITagRepository, TagRepository>();
            prov.Register<ICommentRepository, CommentRepository>();
            prov.Register<INotificationRepository, NotificationRepository>();
            prov.Register<IPrivateMessageRepository, PrivateMessageRepository>();
            prov.Register<IMapper, TagKidMapper>(new TagKidMapper());

            var lines = File.ReadAllLines(@"C:\Users\mehmet\Downloads\QueryResults.csv");

            Console.WriteLine(lines.Length);

            var tags = lines.Select(l => l.Split(',')[1].Trim('"')).ToList();
            var tagChars = tags.SelectMany(c => c).Distinct().OrderBy(c => c);

            foreach (var tag in tagChars)
            {
                Console.Write(tag);
            }

            IEnumerable<Tag> existingTags;
            using (prov.GetOrCreate<IUnitOfWork>())
            {
                var repo = prov.GetOrCreate<ITagRepository>();
                existingTags = repo.GetAll();
            }

            Console.WriteLine();

            var i = 0;

            foreach (var tag in tags.Where(t => existingTags.All(t2 => t != t2.Name)))
            {
                if (++i % 100 == 0)
                    Console.WriteLine(i);

                try
                {
                    var url = String.Format("http://stackoverflow.com/tags/{0}/info", tag.Replace("#", "%23"));

                    var htmlDoc = new HtmlDocument();
                    var downloader = new HtmlDownloader(url);
                    downloader.GetHtml();
                    htmlDoc.LoadHtml(downloader.HtmlContent);

                    var node = htmlDoc.DocumentNode.SelectSingleNode("//div[@id = 'questions']");
                    if (node == null)
                        throw new Exception("id=questions not found");

                    node = node.SelectSingleNode("//div[@class = 'post-text']");
                    if (node == null)
                        throw new Exception("class=post-text not found");

                    var text = node.InnerText.Trim();

                    var hint = text.Length <= 50 ? text : text.Substring(0, 50);
                    var desc = text.Length <= 500 ? text : text.Substring(0, 500);
                    var name = tag;

                    using (var uow = prov.GetOrCreate<IUnitOfWork>())
                    {
                        var repo = prov.GetOrCreate<ITagRepository>();

                        repo.Save(new Tag
                        {
                            Name = name,
                            Hint = hint,
                            Description = desc
                        });

                        uow.Save();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(tag + ": " + ex.GetMessage());
                }
            }
        }
    }
}
