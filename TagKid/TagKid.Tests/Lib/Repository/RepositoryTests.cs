using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taga.Core.IoC;
using Taga.Core.Repository;
using Taga.Core.Repository.Sql;
using TagKid.Lib.Cache;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Models.Filters;
using TagKid.Lib.PetaPoco;
using TagKid.Lib.PetaPoco.Repository;
using TagKid.Lib.PetaPoco.Repository.Sql;
using TagKid.Lib.Repositories;
using TagKid.Lib.Repositories.Impl;
using IMapper = Taga.Core.Repository.IMapper;

namespace TagKid.Tests.Lib.Repository
{
    [TestClass]
    public class RepositoryTests
    {
        [TestInitialize]
        public void Init()
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
        }

        [TestMethod]
        public void TestTagCache()
        {
            var node = TagNameCache.Instance;

            var res = node.Search("");
            Assert.AreEqual(12, res.Count());

            res = node.Search("c");
            Assert.AreEqual(5, res.Count());

            res = node.Search("c-");
            Assert.AreEqual(3, res.Count());

            res = node.Search("c-p");
            Assert.AreEqual(1, res.Count());

            res = node.Search("c-s");
            Assert.AreEqual(2, res.Count());

            res = node.Search("c-pu");
            Assert.AreEqual(0, res.Count());

            res = node.Search("c-plus-plus");
            Assert.AreEqual(1, res.Count());

            res = node.Search("c-sharp");
            Assert.AreEqual(2, res.Count());

            res = node.Search("c-sharps");
            Assert.AreEqual(0, res.Count());

            res = node.Search("c-plus-pluss");
            Assert.AreEqual(0, res.Count());

            res = node.Search("j");
            Assert.AreEqual(5, res.Count());

            res = node.Search("jav");
            Assert.AreEqual(4, res.Count());

            res = node.Search("java");
            Assert.AreEqual(4, res.Count());

            res = node.Search("jq");
            Assert.AreEqual(1, res.Count());
        }

        [TestMethod]
        public void InitTags()
        {
            var tags = new[]
            {
                new Tag {Name = "java", Hint = "programming language"},
                new Tag {Name = "javascript", Hint = "scripting language"},
                new Tag {Name = "jquery", Hint = "javascript library"},
                new Tag {Name = "knockoutjs", Hint = "javascript mvvm library"},
                new Tag {Name = "angularjs", Hint = "javascript mvvm library"},
                new Tag {Name = "c-plus-plus", Hint = "programming language"},
                new Tag {Name = "c", Hint = "programming language"},
                new Tag {Name = "c-sharp", Hint = "programming language"},
                new Tag {Name = "c", Hint = "music note"},
                new Tag {Name = "c-sharp", Hint = "music note"},
                new Tag {Name = "java", Hint = "an island"},
                new Tag {Name = "java", Hint = "a coffee type"}
            };

            using (var uow = ServiceProvider.Provider.GetOrCreate<IUnitOfWork>())
            {
                var repo = ServiceProvider.Provider.GetOrCreate<ITagRepository>();
                foreach (var tag in tags)
                {
                    tag.Description = tag.Hint;
                    tag.Status = TagStatus.Active;
                    repo.Save(tag);
                }
                uow.Save();
            }
        }

        [TestMethod]
        public void TestSearch()
        {
            using (ServiceProvider.Provider.GetOrCreate<IUnitOfWork>())
            {
                var repo = ServiceProvider.Provider.GetOrCreate<IPostRepository>();

                repo.Search(new PostFilter
                {
                    PageIndex = 1,
                    PageSize = 10,
                    UserId = 3,
                    CategoryId = 12,
                    TagIds = new[] { 10L, 11L, 15L },
                    Title = "Token",
                    CategoryAccessLevels = new[] { AccessLevel.Public, AccessLevel.Protected },
                    PostAccessLevels = new[] { AccessLevel.Public, AccessLevel.Protected }
                });
            }
        }

        [TestMethod]
        public void TestLike()
        {
            using (ServiceProvider.Provider.GetOrCreate<IUnitOfWork>())
            {
                var repo = ServiceProvider.Provider.GetOrCreate<IPostRepository>();

                repo.Search(new PostFilter
                {
                    PageIndex = 1,
                    PageSize = 10,
                    Title = "Token"
                });
            }
        }

        [TestMethod]
        public void TestGetLastSuccessfulLogins()
        {
            using (ServiceProvider.Provider.GetOrCreate<IUnitOfWork>())
            {
                var repo = ServiceProvider.Provider.GetOrCreate<ILoginRepository>();
                repo.GetLastSuccessfulLogin(1);
            }
        }

        [TestMethod]
        public void TestGetLogins()
        {
            using (ServiceProvider.Provider.GetOrCreate<IUnitOfWork>())
            {
                var repo = ServiceProvider.Provider.GetOrCreate<ILoginRepository>();
                repo.GetLogins("username", "email", true, 1, 10);
            }
        }

        [TestMethod]
        public void TestGetComments()
        {
            using (ServiceProvider.Provider.GetOrCreate<IUnitOfWork>())
            {
                var repo = ServiceProvider.Provider.GetOrCreate<ICommentRepository>();
                repo.GetByPostId(1, 1, 10);
            }
        }

        [TestMethod]
        public void GetNotifications()
        {
            using (ServiceProvider.Provider.GetOrCreate<IUnitOfWork>())
            {
                var repo = ServiceProvider.Provider.GetOrCreate<INotificationRepository>();
                repo.GetByToUserId(1, false, 1, 10);
            }
        }

        [TestMethod]
        public void GetPrivateMessages()
        {
            using (ServiceProvider.Provider.GetOrCreate<IUnitOfWork>())
            {
                var repo = ServiceProvider.Provider.GetOrCreate<IPrivateMessageRepository>();
                repo.GetMessages(1, 2, 1, 10);
            }
        }

        [TestMethod]
        public void FillTestData()
        {

        }

        [TestMethod]
        public void TestDynamicAnonymous()
        {
            object o = new { X = 3 };
            dynamic d = o;
            Assert.AreEqual(3, d.X);
        }
    }
}
