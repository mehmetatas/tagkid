using System.Data;
using System.Linq;
using Taga.Core.Repository;
using TagKid.Core.Models.Database;
using TagKid.Core.Repositories;

namespace TagKid.Repository
{
    public class PrivateMessageRepository : IPrivateMessageRepository
    {
        private readonly IRepository _repository;

        public PrivateMessageRepository(IRepository repository)
        {
            _repository = repository;
        }

        public IPage<PrivateMessage> GetMessages(long user1, long user2, int pageIndex, int pageSize)
        {
            return _repository.Select<PrivateMessage>()
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