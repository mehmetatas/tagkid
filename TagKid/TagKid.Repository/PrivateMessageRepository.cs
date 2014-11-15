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

        public PrivateMessage[] GetMessages(long loggedInUserId, long otherUserId, int maxCount, long maxMessageId = 0)
        {
            var query = _repository.Select<PrivateMessage>()
                .Where(pm =>
                    (pm.FromUserId == loggedInUserId && pm.ToUserId == otherUserId) ||
                    (pm.FromUserId == otherUserId && pm.ToUserId == loggedInUserId));

            if (maxMessageId > 0)
            {
                query = query.Where(pm => pm.Id < maxMessageId);
            }

            return query.OrderByDescending(pm => pm.Id)
                .Take(maxCount)
                .ToArray();
        }

        public void Save(PrivateMessage privateMessage)
        {
            _repository.Insert(privateMessage);
        }
    }
}