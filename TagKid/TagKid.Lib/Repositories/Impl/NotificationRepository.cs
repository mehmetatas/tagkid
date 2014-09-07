using Taga.Core.Repository;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Models.Entities.Views;
using TagKid.Lib.Utils;

namespace TagKid.Lib.Repositories.Impl
{
    public class NotificationRepository : INotificationRepository
    {
        public IPage<NotificationView> GetByToUserId(long toUserId, bool onlyUnread, int pageIndex, int pageSize)
        {
            var statusArr = onlyUnread
                ? new object[] { NotificationStatus.Unread }
                : new object[] { NotificationStatus.Unread, NotificationStatus.Read };

            var sqlBuilder = Db.SqlBuilder();

            sqlBuilder.SelectAllFrom("notification_view")
                .Where()
                .Equals("to_user_id", toUserId)
                .And().In("status", statusArr);

            return Db.SqlRepository().ExecuteQuery<NotificationView>(sqlBuilder.Build(), pageIndex, pageSize);
        }

        public void Save(Notification notification)
        {
            Db.SqlRepository().Save(notification);
        }
    }
}
