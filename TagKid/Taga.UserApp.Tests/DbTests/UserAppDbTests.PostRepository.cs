using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Taga.UserApp.Core.Database;
using Taga.UserApp.Core.Model.Database;
using Taga.UserApp.Core.Repository;

namespace Taga.UserApp.Tests.DbTests
{
    public abstract partial class UserAppDbTests
    {
        [TestMethod, TestCategory("PostRepository")]
        public void Should_Create_Category()
        {
            User user;
            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<IUserRepository>();

                user = new User
                {
                    Username = "taga",
                    Password = "1234"
                };

                repo.Save(user);
                db.Save();
            }
            
            var time = DateTime.Now;
            time = new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second);

            Category category;
            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<IPostRepository>();

                category = new Category
                {
                    UserId = user.Id,
                    Title = "Test Category",
                    Description = "Description",
                    CreateDate = time
                };

                repo.Save(category);
                db.Save();
            }

            using (var db = Db.Readonly())
            {
                var repo = db.GetRepository<IPostRepository>();

                category = repo.GetCategory(category.Id);
            }

            Assert.AreEqual(user.Id, category.UserId);
            Assert.AreEqual("Test Category", category.Title);
            Assert.AreEqual("Description", category.Description);
            Assert.AreEqual(time, category.CreateDate);
        }

        [TestMethod, TestCategory("PostRepository")]
        public void Should_Update_Category()
        {
            User user;
            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<IUserRepository>();

                user = new User
                {
                    Username = "taga",
                    Password = "1234"
                };

                repo.Save(user);
                db.Save();
            }

            var time = DateTime.Now;
            time = new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second);

            Category category;
            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<IPostRepository>();

                category = new Category
                {
                    UserId = user.Id,
                    Title = "Test Category",
                    Description = "Description",
                    CreateDate = time
                };

                repo.Save(category);
                db.Save();
            }

            var id = category.Id;

            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<IPostRepository>();

                category = repo.GetCategory(category.Id);

                category.Title = "Test Category-Updated";
                category.Description = "Description-Updated";
                category.CreateDate = time.AddDays(1);

                repo.Save(category);
                db.Save();
            }

            using (var db = Db.Readonly())
            {
                var repo = db.GetRepository<IPostRepository>();

                category = repo.GetCategory(id);
            }

            Assert.AreEqual(user.Id, category.UserId);
            Assert.AreEqual("Test Category-Updated", category.Title);
            Assert.AreEqual("Description-Updated", category.Description);
            Assert.AreEqual(time.AddDays(1), category.CreateDate);
        }

        [TestMethod, TestCategory("PostRepository")]
        public void Should_Delete_Category()
        {
            User user;
            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<IUserRepository>();

                user = new User
                {
                    Username = "taga",
                    Password = "1234"
                };

                repo.Save(user);
                db.Save();
            }

            var time = DateTime.Now;
            time = new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second);

            Category category;
            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<IPostRepository>();

                category = new Category
                {
                    UserId = user.Id,
                    Title = "Test Category",
                    Description = "Description",
                    CreateDate = time
                };

                repo.Save(category);
                db.Save();
            }

            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<IPostRepository>();

                repo.Delete(new Category
                {
                    Id = category.Id
                });

                db.Save();
            }

            using (var db = Db.Readonly())
            {
                var repo = db.GetRepository<IPostRepository>();

                category = repo.GetCategory(category.Id);
            }

            Assert.IsNull(category);
        }
    }
}
