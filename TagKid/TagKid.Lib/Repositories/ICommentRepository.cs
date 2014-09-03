using Taga.Core.Repository;
using TagKid.Lib.Entities;

namespace TagKid.Lib.Repositories
{
    public interface ICommentRepository
    {
        IPage<Comment> GetByPostId(long postId, int pageIndex, int pageSize); 

        void Save(Comment comment);
    }
}
