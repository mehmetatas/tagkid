using TagKid.Core.Models.Database;

namespace TagKid.Core.Repository
{
    public interface ICommentRepository : ITagKidRepository
    {
        Comment Get(long commentId);

        int GetCommentCount(long postId);

        Comment[] GetComments(long postId, int maxCount, long maxCommentId = 0);

        void Save(Comment comment);
    }
}