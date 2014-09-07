using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taga.Core.IoC;
using Taga.Core.Repository;
using Taga.Core.Repository.Linq;
using Taga.Core.Repository.Linq.Sql;
using Taga.Core.Repository.Sql;
using TagKid.Lib.Entities;
using TagKid.Lib.Entities.Filters;
using TagKid.Lib.PetaPoco.Repository;
using TagKid.Lib.PetaPoco.Repository.Linq;
using TagKid.Lib.PetaPoco.Repository.Sql;
using TagKid.Lib.Repositories;
using TagKid.Lib.Repositories.Impl;

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
            prov.Register<ILinqRepository, PetaPocoLinqRepository>();
            prov.Register<IUnitOfWork, PetaPocoUnitOfWork>();
            prov.Register<ISqlBuilder, PetaPocoSqlBuilder>();
            prov.Register<IPostRepository, PostRepository>();
            prov.Register<ILoginRepository, LoginRepository>();
            prov.Register<ICommentRepository, CommentRepository>();
            prov.Register<ILinqSqlSchemaSolver, PetaPocoSqlSchemaSolver>();
            prov.Register(typeof(ILinqQueryBuilder<>), typeof(LinqSqlQueryBuilder<>));
        }

        [TestMethod]
        public void TestSearch()
        {
            using (var uow = ServiceProvider.Provider.GetOrCreate<IUnitOfWork>())
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

                uow.Save();
            }
        }

        [TestMethod]
        public void TestLike()
        {
            using (var uow = ServiceProvider.Provider.GetOrCreate<IUnitOfWork>())
            {
                var repo = ServiceProvider.Provider.GetOrCreate<IPostRepository>();

                repo.Search(new PostFilter
                {
                    PageIndex = 1,
                    PageSize = 10,
                    Title = "Token"
                });

                uow.Save();
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
        public void TestDynamicAnonymous()
        {
            object o = new { X = 3 };
            dynamic d = o;
            Assert.AreEqual(3, d.X);
        }
    }
}
