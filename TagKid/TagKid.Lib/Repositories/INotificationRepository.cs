using Taga.Core.Repository;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Models.Entities.Views;

namespace TagKid.Lib.Repositories
{
    public interface INotificationRepository : ITagKidRepository
    {
        IPage<NotificationView> GetByToUserId(long toUserId, bool onlyUnread, int pageIndex, int pageSize);

        void Save(Notification notification);
    }
}