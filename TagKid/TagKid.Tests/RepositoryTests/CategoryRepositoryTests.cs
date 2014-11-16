using Microsoft.VisualStudio.TestTools.UnitTesting;
using TagKid.Core.Database;
using TagKid.Core.Models.Database;
using TagKid.Core.Repository;

namespace TagKid.Tests.RepositoryTests
{
    [TestClass]
    public class CategoryRepositoryTests : BaseRepositoryTests
    {
        [TestMethod, TestCategory("RepositoryTests")]
        public void Should_Create_Category()
        {
            var cat = new Category
            {
                AccessLevel = AccessLevel.Protected,
                Description = "Description",
                Name = "Name",
                Status = CategoryStatus.Active,
                UserId = 3
            };

            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<ICategoryRepository>();
                repo.Save(cat);
                db.Save();
            }

            Category cat2;

            using (var db = Db.Readonly())
            {
                var repo = db.GetRepository<ICategoryRepository>();
                cat2 = repo.GetById(cat.Id);
            }

            Assert.IsNotNull(cat2);
            Assert.AreEqual(cat.AccessLevel, cat2.AccessLevel);
            Assert.AreEqual(cat.Description, cat2.Description);
            Assert.AreEqual(cat.Id, cat2.Id);
            Assert.AreEqual(cat.Name, cat2.Name);
            Assert.AreEqual(cat.Status, cat2.Status);
            Assert.AreEqual(cat.UserId, cat2.UserId);
        }

        [TestMethod, TestCategory("RepositoryTests")]
        public void Should_Update_Category()
        {
            var cat = new Category
            {
                AccessLevel = AccessLevel.Protected,
                Description = "Description",
                Name = "Name",
                Status = CategoryStatus.Active,
                UserId = 3
            };

            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<ICategoryRepository>();
                repo.Save(cat);
                db.Save();
            }

            var id = cat.Id;

            cat = new Category
            {
                Id = id,
                AccessLevel = AccessLevel.Public,
                Description = "Description - Updated",
                Name = "Name - Updated",
                Status = CategoryStatus.Passive,
                UserId = 6
            };

            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<ICategoryRepository>();
                repo.Save(cat);
                db.Save();
            }

            Category cat2;
            using (var db = Db.Readonly())
            {
                var repo = db.GetRepository<ICategoryRepository>();
                cat2 = repo.GetById(cat.Id);
            }

            Assert.IsNotNull(cat2);
            Assert.AreEqual(cat.AccessLevel, cat2.AccessLevel);
            Assert.AreEqual(cat.Description, cat2.Description);
            Assert.AreEqual(cat.Id, cat2.Id);
            Assert.AreEqual(cat.Name, cat2.Name);
            Assert.AreEqual(cat.Status, cat2.Status);
            Assert.AreEqual(cat.UserId, cat2.UserId);
        }

        [TestMethod, TestCategory("RepositoryTests")]
        public void Should_Get_Categories_By_User_Id()
        {
            Category cat = null;

            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<ICategoryRepository>();
                for (var i = 0; i < 10; i++)
                {
                    cat = new Category
                    {
                        AccessLevel = AccessLevel.Protected,
                        Description = "Description",
                        Name = "Name",
                        Status = CategoryStatus.Active,
                        UserId = (i%2) + 1
                    };
                    repo.Save(cat);
                }
                db.Save();
            }

            Category[] cats;

            using (var db = Db.Readonly())
            {
                var repo = db.GetRepository<ICategoryRepository>();
                cats = repo.GetCategories(1);
            }

            Assert.AreEqual(5, cats.Length);
            foreach (var cat2 in cats)
            {
                Assert.AreEqual(1, cat2.UserId);
                Assert.AreEqual(cat.AccessLevel, cat2.AccessLevel);
                Assert.AreEqual(cat.Description, cat2.Description);
                Assert.AreEqual(cat.Name, cat2.Name);
                Assert.AreEqual(cat.Status, cat2.Status);
            }
        }
    }
}