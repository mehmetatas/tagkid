using Taga.Core.Repository;
using TagKid.Lib.Entities;

namespace TagKid.Lib.Repositories
{
    public interface INotificationRepository
    {
        IPage<Notification> GetByToUserId(long toUserId, bool onlyUnread, int pageIndex, int pageSize);

        void Save(Notification notification);
    }
}
