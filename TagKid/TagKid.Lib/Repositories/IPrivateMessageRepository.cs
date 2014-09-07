using Taga.Core.Repository;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Models.Entities.Views;

namespace TagKid.Lib.Repositories
{
    public interface IPrivateMessageRepository
    {
        IPage<PrivateMessageView> GetMessages(long user1, long user2, int pageIndex, int pageSize); 

        void Save(PrivateMessage privateMessage);
    }
}
