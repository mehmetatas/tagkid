using Taga.Core.Repository;
using TagKid.Lib.Entities;

namespace TagKid.Lib.Repositories
{
    public interface IPrivateMessageRepository
    {
        IPage<PrivateMessage> GetMessages(long user1, long user2, int pageIndex, int pageSize); 

        void Save(PrivateMessage privateMessage);
    }
}
