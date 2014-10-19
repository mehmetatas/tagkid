using Taga.Core.Repository;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Models.Entities.Views;

namespace TagKid.Lib.Repositories
{
    public interface ICommentRepository : ITagKidRepository
    {
        IPage<CommentView> GetByPostId(long postId, int pageIndex, int pageSize); 

        void Save(Comment comment);
    }
}
