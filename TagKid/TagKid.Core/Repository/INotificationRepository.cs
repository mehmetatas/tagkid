using Taga.Core.Repository;
using TagKid.Core.Models.Database;

namespace TagKid.Core.Repository
{
    public interface INotificationRepository : ITagKidRepository
    {
        Notification GetNotification(long notificationId);

        int GetUnreadNotificationCount(long toUserId);

        IPage<Notification> GetNotifications(long toUserId, bool onlyUnread, int pageIndex, int pageSize);

        void Save(Notification notification);
    }
}