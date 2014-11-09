using Taga.Core.Repository;
using TagKid.Core.Models.Database;

namespace TagKid.Core.Repositories
{
    public interface IPrivateMessageRepository : ITagKidRepository
    {
        IPage<PrivateMessage> GetMessages(long user1, long user2, int pageIndex, int pageSize);

        void Save(PrivateMessage privateMessage);
    }
}