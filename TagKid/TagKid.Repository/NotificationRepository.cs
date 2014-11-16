using System.Linq;
using Taga.Core.Repository;
using TagKid.Core.Models.Database;
using TagKid.Core.Repository;

namespace TagKid.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IRepository _repository;

        public NotificationRepository(IRepository repository)
        {
            _repository = repository;
        }

        public Notification GetNotification(long notificationId)
        {
            var notif = _repository.Select<Notification>()
                .FirstOrDefault(n => n.Id == notificationId);

            if (notif == null)
            {
                return null;
            }

            SetFromUsers(notif);

            return notif;
        }

        public int GetUnreadNotificationCount(long toUserId)
        {
            return _repository.Select<Notification>()
                .Count(n => n.ToUserId == toUserId && n.Status == NotificationStatus.Unread);
        }

        public IPage<Notification> GetNotifications(long toUserId, bool onlyUnread, int pageIndex, int pageSize)
        {
            var query = _repository.Select<Notification>()
                .Where(nv => nv.ToUserId == toUserId);

            query = onlyUnread
                ? query.Where(nv => nv.Status == NotificationStatus.Unread)
                : query.Where(nv => nv.Status != NotificationStatus.Deleted);

            var page = query.Page(pageIndex, pageSize);

            SetFromUsers(page.Items);

            return page;
        }

        public void Save(Notification notification)
        {
            _repository.Save(notification);
        }

        private void SetFromUsers(params Notification[] notifications)
        {
            if (notifications.Length == 0)
            {
                return;
            }

            var fromUserIds = notifications.Select(n => n.FromUserId).Distinct();

            var users = _repository.Select<User>()
                .Where(u => fromUserIds.Contains(u.Id));

            foreach (var notification in notifications)
            {
                notification.FromUser = users.First(u => u.Id == notification.FromUserId);
            }
        }
    }
}