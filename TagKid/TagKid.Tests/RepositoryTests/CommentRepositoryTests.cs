using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TagKid.Core.Database;
using TagKid.Core.Models.Database;
using TagKid.Core.Repositories;
using TagKid.Core.Utils;

namespace TagKid.Tests.RepositoryTests
{
    [TestClass]
    public class CommentRepositoryTests : BaseRepositoryTests
    {
        [TestMethod, TestCategory("RepositoryTests")]
        public void Should_Create_And_Find_Comments()
        {
            var updateDate = DateTime.Now.TrimMillis();
            var publishDate = updateDate.AddDays(-1).TrimMillis();

            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<IUserRepository>();
                for (var i = 0; i < 5; i++)
                {
                    repo.Save(new User
                    {
                        JoinDate = DateTime.Now.TrimMillis()
                    });
                }
                db.Save();
            }

            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<ICommentRepository>();

                // i              0  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17 18 19
                // CommentId      1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17 18 19 20
                // PostId         1  2  1  2  1  2  1  2  1  2  1  2  1  2  1  2  1  2  1  2
                // CommentStatus  1  0  1  1  1  0  1  1  1  0  1  1  1  0  1  1  1  0  1  1
                // UserId         1  2  3  4  5  1  2  3  4  5  1  2  3  4  5  1  2  3  4  5

                for (var i = 0; i < 20; i++)
                {
                    var comment = new Comment
                    {
                        Content = "Content",
                        PostId = (i%2) + 1,
                        PublishDate = publishDate,
                        UpdateDate = updateDate,
                        Status = i%4 == 1 ? CommentStatus.Deleted : CommentStatus.Active,
                        UserId = (i%5) + 1
                    };

                    repo.Save(comment);
                }
                db.Save();
            }

            int post1CommentCount;
            int post2CommentCount;

            Comment[] commentsOfPost11;
            Comment[] commentsOfPost12;
            Comment[] commentsOfPost21;
            Comment[] commentsOfPost22;

            using (var db = Db.Readonly())
            {
                var repo = db.GetRepository<ICommentRepository>();

                post1CommentCount = repo.GetCommentCount(1);
                post2CommentCount = repo.GetCommentCount(2);

                commentsOfPost11 = repo.GetComments(1, 5);
                commentsOfPost12 = repo.GetComments(1, 5, commentsOfPost11.Min(c => c.Id));
                commentsOfPost21 = repo.GetComments(2, 5);
                commentsOfPost22 = repo.GetComments(2, 5, commentsOfPost21.Min(c => c.Id));
            }

            Assert.AreEqual(10, post1CommentCount);
            Assert.AreEqual(5, post2CommentCount);
            Assert.AreEqual(5, commentsOfPost11.Length);
            Assert.AreEqual(5, commentsOfPost12.Length);
            Assert.AreEqual(5, commentsOfPost21.Length);
            Assert.AreEqual(0, commentsOfPost22.Length);
        }
    }
}