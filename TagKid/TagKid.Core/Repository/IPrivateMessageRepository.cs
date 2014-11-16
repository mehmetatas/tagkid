using TagKid.Core.Models.Database;

namespace TagKid.Core.Repository
{
    public interface IPrivateMessageRepository : ITagKidRepository
    {
        PrivateMessage GetMessage(long messageId);

        PrivateMessage[] GetMessages(long user1, long user2, int maxCount, long maxMessageId = 0);

        void Save(PrivateMessage privateMessage);
    }
}