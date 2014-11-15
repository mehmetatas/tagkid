using Taga.Core.Repository;
using TagKid.Core.Models.Database;

namespace TagKid.Core.Repositories
{
    public interface ICommentRepository : ITagKidRepository
    {
        int GetCommentCount(long postId);

        Comment[] GetComments(long postId, int maxCount, long maxCommentId = 0);

        void Save(Comment comment);
    }
}