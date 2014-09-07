using Taga.Core.Repository;
using TagKid.Lib.Entities;
using TagKid.Lib.Entities.Views;
using TagKid.Lib.Utils;

namespace TagKid.Lib.Repositories.Impl
{
    public class CommentRepository : ICommentRepository
    {
        public IPage<CommentView> GetByPostId(long postId, int pageIndex, int pageSize)
        {
            return Db.LinqRepository().Query(Db.LinqQueryBuilder<CommentView>()
                .Where(c => c.PostId == postId)
                .OrderBy(c => c.Id, true)
                .Page(pageIndex, pageSize));
        }

        public void Save(Comment comment)
        {
            Db.LinqRepository().Save(comment);
        }
    }
}
