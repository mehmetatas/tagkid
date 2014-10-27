using System.Linq;
using Taga.Core.Repository;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Models.Entities.Views;

namespace TagKid.Lib.Repositories.Impl
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IRepository _repository;

        public NotificationRepository(IRepository repository)
        {
            _repository = repository;
        }

        public IPage<NotificationView> GetByToUserId(long toUserId, bool onlyUnread, int pageIndex, int pageSize)
        {
            var query = _repository.Select<NotificationView>()
                .Where(nv => nv.ToUserId == toUserId);

            query = onlyUnread
                ? query.Where(nv => nv.Status == NotificationStatus.Unread)
                : query.Where(nv => nv.Status != NotificationStatus.Deleted);

            return query.Page(pageIndex, pageSize);
        }

        public void Save(Notification notification)
        {
            _repository.Save(notification);
        }
    }
}