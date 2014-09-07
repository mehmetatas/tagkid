using Taga.Core.Repository;
using TagKid.Lib.Entities;
using TagKid.Lib.Entities.Views;

namespace TagKid.Lib.Repositories
{
    public interface ICommentRepository
    {
        IPage<CommentView> GetByPostId(long postId, int pageIndex, int pageSize); 

        void Save(Comment comment);
    }
}
