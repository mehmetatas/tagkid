using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Tool.hbm2ddl;

namespace TagKid.NHTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var sessionFactory = BuildSessionFactory<PostMap>("test");

                //using (var session = sessionFactory.OpenSession())
                //{
                //    using (var tx = session.BeginTransaction())
                //    {
                //        var tag1 = new Tag { Name = "Tag1" };
                //        var tag2 = new Tag { Name = "Tag2" };
                //        var user1 = new User { Username = "User1" };
                //        var user2 = new User { Username = "User2" };

                //        session.Save(tag1);
                //        session.Save(tag2);
                //        session.Save(user1);
                //        session.Save(user2);

                //        var post = new Post
                //        {
                //            Title = "Post1",
                //            User = user1,
                //            Tags = new List<Tag> { tag1 }
                //        };

                //        var post2 = new Post
                //        {
                //            Title = "Post2",
                //            User = user2,
                //            Tags = new List<Tag> { tag1 }
                //        };

                //        session.Save(post);
                //        session.Save(post2);

                //        tx.Commit();
                //    }
                //}

                //using (var session = sessionFactory.OpenSession())
                //{
                //    using (var tx = session.BeginTransaction())
                //    {
                //        var post = new Post
                //        {
                //            Id = 1,
                //            Title = "Title Updated",
                //            User = new User { Id = 1 },
                //            Tags = new[] { new Tag { Id = 2 } }
                //        };

                //        session.Update(post);

                //        tx.Commit();
                //    }
                //}

                //using (var session = sessionFactory.OpenSession())
                //{
                //    using (var tx = session.BeginTransaction())
                //    {
                //        var like = new Like
                //        {
                //            Post = new Post { Id = 1 },
                //            User = new User { Id = 1 }
                //        };
                //        session.Save(like);

                //        like = new Like
                //        {
                //            Post = new Post { Id = 1 },
                //            User = new User { Id = 2 }
                //        };
                //        session.Save(like);

                //        like = new Like
                //        {
                //            Post = new Post { Id = 2 },
                //            User = new User { Id = 1 }
                //        };
                //        session.Save(like);

                //        tx.Commit();
                //    }
                //}

                using (var session = sessionFactory.OpenStatelessSession())
                {
                    var post = session.Query<Post>()
                        .Fetch(p => p.User)
                        .ToList();

                    Console.WriteLine(post[0].User.Id);
                    Console.WriteLine(post[0].User.Username);
                    Console.WriteLine(post[0].Tags == null);
                    Console.WriteLine(post[0].Likes == null);
                    Console.WriteLine(post[0].Tags.Count);
                    Console.WriteLine(post[0].Likes.Count);
                }

                Console.WriteLine("OK!");
            }
            catch (Exception e)
            {
                while (e != null)
                {
                    Console.WriteLine(e.Message);
                    e = e.InnerException;
                }
            }
            Console.ReadLine();
        }

        private static ISessionFactory BuildSessionFactory<T>(string connectionStringName)
        {
            var fluentConfiguration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(c => c.FromConnectionStringWithKey(connectionStringName)))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<T>());

            fluentConfiguration.ExposeConfiguration(c => new SchemaUpdate(c).Execute(false, true));

            var fluentBuildUpConfiguration = fluentConfiguration.BuildConfiguration();

            SchemaMetadataUpdater.QuoteTableAndColumns(fluentBuildUpConfiguration);

            return fluentBuildUpConfiguration.BuildSessionFactory();
        }
    }
    
    public class OneToManyFetcher
    {
    
    }

    public class PostLikeFetcher
    {
        private readonly IStatelessSession _session;

        public PostLikeFetcher(IStatelessSession session)
        {
            _session = session;
        }

        public void Fetch(params Post[] posts)
        {
            var postIds = posts.Select(p => p.Id);

            var likes = _session.Query<Like>()
                .Where(l => postIds.Contains(l.Post.Id))
                .ToList();

            foreach (var post in posts)
            {
                post.Likes = likes.Where(l => l.Post.Id == post.Id).ToList();
            }
        }
    }

    public class PostTagFetcher
    {
        private readonly IStatelessSession _session;

        public PostTagFetcher(IStatelessSession session)
        {
            _session = session;
        }

        public void Fetch(params Post[] posts)
        {
            var postIds = posts.Select(p => p.Id);

            var likes = _session.Query<Like>()
                .Where(l => postIds.Contains(l.Post.Id))
                .ToList();

            foreach (var post in posts)
            {
                post.Likes = likes.Where(l => l.Post.Id == post.Id).ToList();
            }
        }
    }
}
