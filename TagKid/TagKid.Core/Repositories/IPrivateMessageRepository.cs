using TagKid.Core.Models.Database;

namespace TagKid.Core.Repositories
{
    public interface IPrivateMessageRepository : ITagKidRepository
    {
        PrivateMessage[] GetMessages(long user1, long user2, int maxCount, long maxMessageId = 0);

        void Save(PrivateMessage privateMessage);
    }
}