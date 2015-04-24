using System;
using System.Linq;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace TagKid.NHTestApp
{
    class Program
    {
        static void Main()
        {
            try
            {
                Fetchers.RegisterOneToMany<Post, Like>(p => p.Likes,
                    postIds => (like => postIds.Contains(like.Post.Id)),
                    post => (like => like.Post.Id == post.Id),
                    (post, likes) => post.Likes = likes);

                Fetchers.RegisterManyToMany<Post, Tag, PostTag>(p => p.Tags,
                    postIds => (pt => postIds.Contains(pt.Post.Id)),
                    () => (pt => new ManyToManyItem<Tag> { ParentId = pt.Post.Id, Child = pt.Tag }),
                    (post, tags) => post.Tags = tags);

                var sessionFactory = BuildSessionFactory<PostMap>("test");

                //using (var repo = new NHRepository(sessionFactory))
                //{
                //    var tag1 = new Tag { Name = "Tag1" };
                //    var tag2 = new Tag { Name = "Tag2" };
                //    var user1 = new User { Username = "User1" };
                //    var user2 = new User { Username = "User2" };

                //    repo.Insert(tag1);
                //    repo.Insert(tag2);
                //    repo.Insert(user1);
                //    repo.Insert(user2);

                //    var post1 = new Post
                //    {
                //        Title = "Post1",
                //        User = user1,
                //        Tags = new List<Tag> { tag1 }
                //    };

                //    var post2 = new Post
                //    {
                //        Title = "Post2",
                //        User = user2,
                //        Tags = new List<Tag> { tag1 }
                //    };

                //    repo.Insert(post1);
                //    repo.Insert(post2);

                //    repo.Insert(new PostTag
                //    {
                //        Post = post1,
                //        Tag = tag1
                //    });

                //    repo.Insert(new PostTag
                //    {
                //        Post = post1,
                //        Tag = tag2
                //    });

                //    repo.Insert(new PostTag
                //    {
                //        Post = post2,
                //        Tag = tag1
                //    });

                //    repo.Insert(new Like
                //    {
                //        Post = new Post { Id = 1 },
                //        User = new User { Id = 1 }
                //    });

                //    repo.Insert(new Like
                //    {
                //        Post = new Post { Id = 1 },
                //        User = new User { Id = 2 }
                //    });

                //    repo.Insert(new Like
                //    {
                //        Post = new Post { Id = 2 },
                //        User = new User { Id = 1 }
                //    });
                //}

                //using (var repo = new NHRepository(sessionFactory))
                //{
                //    var post = new Post
                //    {
                //        Id = 1,
                //        Title = "Title Updated",
                //        User = new User { Id = 1 },
                //        Tags = new[] { new Tag { Id = 2 } }
                //    };

                //    repo.Update(post);
                //}

                using (var repo = new NHRepository(sessionFactory))
                {
                    var posts = repo.Select<Post>()
                        .Join(p => p.User)
                        .ToList();

                    repo.Fetch(p => p.Likes, posts);
                    repo.Fetch(p => p.Tags, posts);

                    foreach (var post in posts)
                    {
                        Console.WriteLine(post.User.Id);
                        Console.WriteLine(post.User.Username);
                        Console.WriteLine(post.Tags == null);
                        Console.WriteLine(post.Likes == null);
                        Console.WriteLine(post.Tags.Count);
                        Console.WriteLine(post.Likes.Count);
                    }
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
}
