using System.Linq;
using Taga.Core.Repository;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Models.Entities.Views;

namespace TagKid.Lib.Repositories.Impl
{
    public class PrivateMessageRepository : IPrivateMessageRepository
    {
        private readonly IRepository _repository;

        public PrivateMessageRepository(IRepository repository)
        {
            _repository = repository;
        }

        public IPage<PrivateMessageView> GetMessages(long user1, long user2, int pageIndex, int pageSize)
        {
            return _repository.Query<PrivateMessageView>()
                .Where(pm =>
                    (pm.FromUserId == user1 && pm.ToUserId == user2) ||
                    (pm.FromUserId == user2 && pm.ToUserId == user1))
                .OrderByDescending(pm => pm.MessageDate)
                .Page(pageIndex, pageSize);
        }

        public void Save(PrivateMessage privateMessage)
        {
            _repository.Save(privateMessage);
        }
    }
}
