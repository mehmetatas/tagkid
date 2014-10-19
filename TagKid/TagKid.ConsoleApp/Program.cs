using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using Taga.Core.IoC;
using Taga.Core.Repository;
using TagKid.Lib.Bootstrapping;
using TagKid.Lib.Exceptions;
using TagKid.Lib.Repositories;
using Tag = TagKid.Lib.Models.Entities.Tag;

namespace TagKid.ConsoleApp
{
    class Program
    {
        static Taga.Core.IoC.IServiceProvider prov = ServiceProvider.Provider;
        static void Main(string[] args)
        {
            try
            {
                Bootstrapper.StartApp();

                //StacManClient c = new StacManClient("TrhOnlvpT7bZ*Jj5NttVVA((");

                //c.Questions.GetAll(filter:)

                //HashSet<String> existingTags;
                //using (prov.GetOrCreate<IUnitOfWork>())
                //{
                //    var repo = prov.GetOrCreate<ITagRepository>();
                //    var tt = repo.GetAll();
                //    existingTags = new HashSet<String>(tt.Select(t => t.Name));
                //}

                //int pageSize = 100;
                //StackExchange.StacMan.Tag[] arr;

                //var tags = new List<Tag>();

                //int page = 211;
                //int retry = 10;
                //do
                //{
                //    arr = null;
                //    try
                //    {
                //        arr = c.Tags.GetAll("stackoverflow", page: page++, pagesize: pageSize, sort: Sort.Name).Result.Data.Items;
                //        if (arr == null)
                //            break;

                //        foreach (var tag in arr)
                //        {
                //            tags.Add(new Tag
                //            {
                //                Name = tag.Name,
                //                Hint = tag.Name,
                //                Description = tag.Name
                //            });
                //        }
                //        retry = 10;

                //        if (tags.Count % 500 == 0)
                //            Console.WriteLine(tags.Count);
                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine("FATAL: " + ex.GetMessage());
                //        if (retry-- > 0)
                //            arr = new StackExchange.StacMan.Tag[100];
                //    }
                //} while (arr != null && arr.Length == pageSize);

                //Console.WriteLine(tags.Count);
                //using (var uow = prov.GetOrCreate<IUnitOfWork>())
                //{
                //    var repo = prov.GetOrCreate<ITagRepository>();
                //    foreach (var tag in tags)
                //    {
                //        try
                //        {
                //            repo.Save(tag);
                //        }
                //        catch (Exception ex)
                //        {
                //            Console.WriteLine(tag.Name + ": " + ex.GetMessage());
                //        }
                //    }
                //    uow.Save();

                //}

                //DownloadData();
                UpdateTagCounts();

                Console.WriteLine("OK!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }

        private static void UpdateTagCounts()
        {
            var lines = File.ReadAllLines(@"C:\Users\mehmet\Downloads\tags.csv");

            using (var uow = prov.GetOrCreate<IUnitOfWork>())
            {
                var repo = prov.GetOrCreate<ITagRepository>();

                var i = lines.Length;
                Console.WriteLine(i);

                foreach (var line in lines)
                {
                    var ss = line.Split(',');

                    var name = ss[1].Replace("\"", "");
                    var count = Convert.ToInt64(ss[2].Replace("\"", ""));

                    var tag = new Tag { Name = name, Count = count };

                    tag.Hint = tag.Name + " - Hint";
                    tag.Description = tag.Name + " - Description";

                    repo.Save(tag);

                    if (--i % 100 == 0)
                        Console.WriteLine(i);
                }

                uow.Save();
            }
        }

        private static void DownloadData()
        {
            //var lines = File.ReadAllLines(@"C:\Users\mehmet\Downloads\QueryResults.csv");

            //Console.WriteLine(lines.Length);

            //var tags = lines.Select(l => l.Split(',')[1].Trim('"')).ToList();
            //var tagChars = tags.SelectMany(c => c).Distinct().OrderBy(c => c);

            //foreach (var tag in tagChars)
            //{
            //    Console.Write(tag);
            //}

            IEnumerable<Tag> existingTags;
            using (prov.GetOrCreate<IUnitOfWork>())
            {
                var repo = prov.GetOrCreate<ITagRepository>();
                existingTags = repo.GetAll();
            }

            Console.WriteLine();

            var i = 0;

            foreach (var tag in existingTags)
            {
                if (++i % 100 == 0)
                    Console.WriteLine(i);

                try
                {
                    var url = String.Format("http://stackoverflow.com/tags/{0}/info", tag.Name.Replace("#", "%23"));

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

                    using (var uow = prov.GetOrCreate<IUnitOfWork>())
                    {
                        var repo = prov.GetOrCreate<ITagRepository>();

                        tag.Hint = hint;
                        tag.Description = desc;
                        repo.Save(tag);

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
