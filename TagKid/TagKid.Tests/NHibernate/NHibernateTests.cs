using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using Taga.Core.IoC;
using TagKid.Lib.Bootstrapping;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.NHibernate;

namespace TagKid.Tests.NHibernate
{
    [TestClass]
    public class NHibernateTests
    {
        private static ISessionFactory _sessionFactory;

        [ClassInitialize]
        public static void TestInit(TestContext ctx)
        {
            Bootstrapper.StartApp();

            _sessionFactory = ServiceProvider.Provider.GetOrCreate<ISessionFactory>();
        }

        private User CreateUser()
        {
            return new User
            {
                Email = "email@email.com",
                FacebookId = "facebook-id",
                Fullname = "fullname",
                JoinDate = DateTime.Now,
                Password = "password",
                Status = UserStatus.AwaitingActivation,
                Type = UserType.User,
                Username = "username"
            };
        }

        [TestMethod]
        public void ShouldInsertTagPost()
        {
            using (var uow = new NHUnitOfWork(_sessionFactory))
            {
                uow.BeginTransaction();

                var repo = new NHRepository();

                repo.Save(new TagPost {TagId = 1, PostId = 1});

                uow.Save();
            }
        }

        [TestMethod]
        public void ShouldUpdateUser()
        {
            var user = CreateUser();

            user.Id = 1;

            var username = user.Username + "Updated";

            using (var uow = new NHUnitOfWork(_sessionFactory))
            {
                uow.BeginTransaction();

                var repo = new NHRepository();

                repo.Save(user);

                user.Username = username;

                uow.Save();
            }

            using (new NHUnitOfWork(_sessionFactory))
            {
                var repo = new NHRepository();

                user = repo.Query<User>().FirstOrDefault(u => u.Id == user.Id);
            }

            Assert.AreEqual(username, user.Username);
        }

        [TestMethod]
        public void ShouldDeleteUser()
        {
            User user;

            using (var uow = new NHUnitOfWork(_sessionFactory))
            {
                uow.BeginTransaction();

                var repo = new NHRepository();

                user = CreateUser();
                user.Username += "TobeDeleted";

                repo.Save(user);

                uow.Save();
            }

            using (var uow = new NHUnitOfWork(_sessionFactory))
            {
                uow.BeginTransaction();

                var repo = new NHRepository();

                repo.Delete(user);

                uow.Save();
            }

            using (new NHUnitOfWork(_sessionFactory))
            {
                var repo = new NHRepository();

                user = repo.Query<User>().FirstOrDefault(u => u.Username == user.Username);
            }

            Assert.IsNull(user);
        }

        [TestMethod]
        public void ShouldInsertUser()
        {
            User user;

            using (var uow = new NHUnitOfWork(_sessionFactory))
            {
                uow.BeginTransaction();

                var repo = new NHRepository();

                user = CreateUser();

                repo.Save(user);

                uow.Save();
            }

            using (new NHUnitOfWork(_sessionFactory))
            {
                var repo = new NHRepository();

                user = repo.Query<User>().FirstOrDefault(u => u.Id == user.Id);
            }

            Assert.AreEqual("username", user.Username);
        }

        [TestMethod]
        public void ShouldSelectUser()
        {
            using (new NHUnitOfWork(_sessionFactory))
            {
                var repo = new NHRepository();
                var users = repo.Query<User>().Where(u => u.Username.StartsWith("M"));

                //var users = repo.ExecSql<User>("select * from users where username like :p_name",
                //    new Dictionary<string, object>
                //    {
                //        {"p_name", "m%"}
                //    });

                //var users = repo.ExecSp<User>("search_users",
                //    new Dictionary<string, object>
                //    {
                //        {"usr", "m"}
                //    });

                foreach (var user in users)
                {
                    Assert.IsTrue(user.Username.StartsWith("m"));
                }
            }
        }
    }
}