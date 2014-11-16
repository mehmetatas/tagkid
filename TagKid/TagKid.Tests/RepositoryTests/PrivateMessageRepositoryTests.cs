using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TagKid.Core.Database;
using TagKid.Core.Models.Database;
using TagKid.Core.Repository;
using TagKid.Core.Utils;

namespace TagKid.Tests.RepositoryTests
{
    [TestClass]
    public class PrivateMessageRepositoryTests : BaseRepositoryTests
    {
        [TestMethod, TestCategory("RepositoryTests")]
        public void Should_Create_And_Find_Private_Messages()
        {
            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<IPrivateMessageRepository>();

                // i              0  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17 18 19
                // Status         0  1  0  1  0  1  0  1  0  1  0  1  0  1  0  1  0  1  0  1 
                // FromUserId     1  2  1  2  1  2  1  2  1  2  1  2  1  2  1  2  1  2  1  2
                // ToUserId       2  1  2  1  2  1  2  1  2  1  2  1  2  1  2  1  2  1  2  1

                for (var i = 0; i < 20; i++)
                {
                    var message = new PrivateMessage
                    {
                        Message = "Message " + i,
                        MessageDate = DateTime.Now.TrimMillis(),
                        Status = (PrivateMessageStatus)(i % 2),
                        FromUserId = (i % 2) + 1,
                        ToUserId = ((i+1) % 2) + 1
                    };

                    repo.Save(message);
                }
                db.Save();
            }

            PrivateMessage[] messages1;
            PrivateMessage[] messages2;
            PrivateMessage[] messages3;

            using (var db = Db.Readonly())
            {
                var repo = db.GetRepository<IPrivateMessageRepository>();

                messages1 = repo.GetMessages(1, 2, 7);
                messages2 = repo.GetMessages(1, 2, 7, messages1.Min(m => m.Id));
                messages3 = repo.GetMessages(1, 2, 7, messages2.Min(m => m.Id));
            }

            Assert.AreEqual(7, messages1.Length);
            Assert.AreEqual(7, messages2.Length);
            Assert.AreEqual(6, messages3.Length);
        }
    }
}