using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taga.Core.Repository;
using TagKid.Core.Database;
using TagKid.Core.Models.Database;
using TagKid.Core.Repository;
using TagKid.Core.Utils;

namespace TagKid.Tests.RepositoryTests
{
    [TestClass]
    public class NotificationRepositoryTests : BaseRepositoryTests
    {
        [TestMethod, TestCategory("RepositoryTests")]
        public void Should_Create_And_Find_Notifications()
        {
            var updateDate = DateTime.Now;
            var publishDate = updateDate.AddDays(-1);

            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<IUserRepository>();
                for (var i = 0; i < 5; i++)
                {
                    repo.Save(new User
                    {
                        Email = "Email " + i,
                        Username = "Username " + i,
                        Password = "Password " + i,
                        Fullname = "Fullname " + i,
                        FacebookId = "FacebookId " + i,
                        JoinDate = DateTime.Now.AddDays(-i).TrimMillis(),
                        Status = (UserStatus)(i % 5),
                        Type = (UserType)(i % 3)
                    });
                }
                db.Save();
            } 
            
            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<INotificationRepository>();

                // i              0  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17 18 19
                // NotifId        1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17 18 19 20   
                // ToUserId       1  2  3  4  5  1  2  3  4  5  1  2  3  4  5  1  2  3  4  5  
                // FromUserId     2  3  4  5  1  2  3  4  5  1  2  3  4  5  1  2  3  4  5  1    
                // NotifStatus    0  1  2  0  1  2  0  1  2  0  1  2  0  1  2  0  1  2  0  1    
                // NotifType      0  1  2  3  4  0  1  2  3  4  0  1  2  3  4  0  1  2  3  4  

                for (var i = 0; i < 20; i++)
                {
                    var notif = new Notification
                    {
                        PostId = i + 1,
                        Status = (NotificationStatus) (i%3),
                        ToUserId = (i%5) + 1,
                        FromUserId = ((i + 1)%5) + 1,
                        Message = "Message " + (i + 1),
                        ActionDate = publishDate,
                        Type = (NotificationType) (i%5)
                    };

                    repo.Save(notif);
                }
                db.Save();
            }

            int unreadCount;
            IPage<Notification> all;
            IPage<Notification> unread;

            using (var db = Db.Readonly())
            {
                var repo = db.GetRepository<INotificationRepository>();

                unreadCount = repo.GetUnreadNotificationCount(1);

                all = repo.GetNotifications(3, false, 1, 10);
                unread = repo.GetNotifications(3, true, 1, 10);
            }

            Assert.AreEqual(2, unreadCount);
            Assert.AreEqual(2, all.TotalCount);
            Assert.AreEqual(1, unread.TotalCount);
        }
    }
}
