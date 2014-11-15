using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Taga.Core.Repository;
using TagKid.Core.Database;
using TagKid.Core.Models.Database;
using TagKid.Core.Repositories;
using TagKid.Core.Utils;

namespace TagKid.Tests.RepositoryTests
{
    [TestClass]
    public class UserRepositoryTests : BaseRepositoryTests
    {
        [TestMethod]
        public void Should_Create_User()
        {
            var user = new User
            {
                Email = "mail@mail.com",
                FacebookId = "123456",
                Fullname = "full-name",
                JoinDate = DateTime.Now.TrimMillis(),
                Password = Util.EncryptPwd("1234"),
                Status = UserStatus.Banned,
                Type = UserType.Moderator,
                Username = "username"
            };

            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<IUserRepository>();
                repo.Save(user);
                db.Save();
            }

            User user2;

            using (var db = Db.Readonly())
            {
                var repo = db.GetRepository<IUserRepository>();
                user2 = repo.GetById(user.Id);
            }

            Assert.IsNotNull(user2);

            Assert.AreEqual(user.Email, user2.Email);
            Assert.AreEqual(user.FacebookId, user2.FacebookId);
            Assert.AreEqual(user.Fullname, user2.Fullname);
            Assert.AreEqual(user.Id, user2.Id);
            Assert.AreEqual(user.JoinDate, user2.JoinDate);
            Assert.AreEqual(user.Password, user2.Password);
            Assert.AreEqual(user.Status, user2.Status);
            Assert.AreEqual(user.Type, user2.Type);
            Assert.AreEqual(user.Username, user2.Username);
        }
        [TestMethod]
        public void Should_Update_User()
        {
            var joinDate = DateTime.Now.TrimMillis();
            var user = new User
            {
                Email = "mail@mail.com",
                FacebookId = "123456",
                Fullname = "full-name",
                JoinDate = joinDate,
                Password = Util.EncryptPwd("1234"),
                Status = UserStatus.Banned,
                Type = UserType.Moderator,
                Username = "username"
            };

            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<IUserRepository>();
                repo.Save(user);
                db.Save();
            }

            var user2 = new User
            {
                Id = user.Id,
                Email = user.Email + " Updated",
                FacebookId = user.FacebookId + " Updated",
                Fullname = user.Fullname + " Updated",
                JoinDate = user.JoinDate.AddDays(1),
                Password = Util.EncryptPwd("1234 Updated"),
                Status = UserStatus.Passive,
                Type = UserType.Admin,
                Username = user.Username + " Updated"
            };

            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<IUserRepository>();
                repo.Save(user2);
                db.Save();
            }

            using (var db = Db.Readonly())
            {
                var repo = db.GetRepository<IUserRepository>();
                user2 = repo.GetById(user.Id);
            }

            Assert.IsNotNull(user2);

            Assert.AreEqual(user.Email + " Updated", user2.Email);
            Assert.AreEqual(user.FacebookId + " Updated", user2.FacebookId);
            Assert.AreEqual(user.Fullname + " Updated", user2.Fullname);
            Assert.AreEqual(user.Id, user2.Id);
            Assert.AreEqual(joinDate.AddDays(1), user2.JoinDate);
            Assert.AreEqual(Util.EncryptPwd("1234 Updated"), user2.Password);
            Assert.AreEqual(UserStatus.Passive, user2.Status);
            Assert.AreEqual(UserType.Admin, user2.Type);
            Assert.AreEqual(user.Username + " Updated", user2.Username);
        }

        [TestMethod]
        public void Should_Get_User_By_Email()
        {
            var user = new User
            {
                Email = "mail@mail.com",
                FacebookId = "123456",
                Fullname = "full-name",
                JoinDate = DateTime.Now.TrimMillis(),
                Password = Util.EncryptPwd("1234"),
                Status = UserStatus.Banned,
                Type = UserType.Moderator,
                Username = "username"
            };

            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<IUserRepository>();
                repo.Save(user);
                db.Save();
            }

            User user2;

            using (var db = Db.Readonly())
            {
                var repo = db.GetRepository<IUserRepository>();
                user2 = repo.GetByEmail(user.Email);
            }

            Assert.IsNotNull(user2);

            Assert.AreEqual(user.Email, user2.Email);
            Assert.AreEqual(user.FacebookId, user2.FacebookId);
            Assert.AreEqual(user.Fullname, user2.Fullname);
            Assert.AreEqual(user.Id, user2.Id);
            Assert.AreEqual(user.JoinDate, user2.JoinDate);
            Assert.AreEqual(user.Password, user2.Password);
            Assert.AreEqual(user.Status, user2.Status);
            Assert.AreEqual(user.Type, user2.Type);
            Assert.AreEqual(user.Username, user2.Username);
        }

        [TestMethod]
        public void Should_Get_User_By_Username()
        {
            var user = new User
            {
                Email = "mail@mail.com",
                FacebookId = "123456",
                Fullname = "full-name",
                JoinDate = DateTime.Now.TrimMillis(),
                Password = Util.EncryptPwd("1234"),
                Status = UserStatus.Banned,
                Type = UserType.Moderator,
                Username = "username"
            };

            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<IUserRepository>();
                repo.Save(user);
                db.Save();
            }

            User user2;

            using (var db = Db.Readonly())
            {
                var repo = db.GetRepository<IUserRepository>();
                user2 = repo.GetByUsername(user.Username);
            }

            Assert.IsNotNull(user2);

            Assert.AreEqual(user.Email, user2.Email);
            Assert.AreEqual(user.FacebookId, user2.FacebookId);
            Assert.AreEqual(user.Fullname, user2.Fullname);
            Assert.AreEqual(user.Id, user2.Id);
            Assert.AreEqual(user.JoinDate, user2.JoinDate);
            Assert.AreEqual(user.Password, user2.Password);
            Assert.AreEqual(user.Status, user2.Status);
            Assert.AreEqual(user.Type, user2.Type);
            Assert.AreEqual(user.Username, user2.Username);
        }

        [TestMethod]
        public void Should_Search_Users()
        {
            var user1 = new User
            {
                Email = "mail@mail.com",
                FacebookId = "123456",
                Fullname = "Mehmet Ataş",
                JoinDate = DateTime.Now.TrimMillis(),
                Password = Util.EncryptPwd("1234"),
                Status = UserStatus.Active,
                Type = UserType.Moderator,
                Username = "matas"
            };

            var user2 = new User
            {
                Email = "email@email.com",
                FacebookId = "123456",
                Fullname = "Fırat Ataş",
                JoinDate = DateTime.Now.TrimMillis(),
                Password = Util.EncryptPwd("1234"),
                Status = UserStatus.Active,
                Type = UserType.Moderator,
                Username = "fatas"
            };

            var user3 = new User
            {
                Email = "email@email.com",
                FacebookId = "123456",
                Fullname = "Seda Çetinkaya",
                JoinDate = DateTime.Now.TrimMillis(),
                Password = Util.EncryptPwd("1234"),
                Status = UserStatus.Banned,
                Type = UserType.Moderator,
                Username = "scetinkaya"
            };

            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<IUserRepository>();
                repo.Save(user1);
                repo.Save(user2);
                repo.Save(user3);
                db.Save();
            }

            IPage<User> page;

            using (var db = Db.Readonly())
            {
                var repo = db.GetRepository<IUserRepository>();
                page = repo.Search("et", 1, 10);
            }

            Assert.AreEqual(1, page.Items.Length);
        }
    }
}
