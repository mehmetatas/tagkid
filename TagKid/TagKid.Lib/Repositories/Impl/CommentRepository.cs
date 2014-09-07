using Taga.Core.Repository;
using Taga.Core.Repository.Sql;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Models.Entities.Views;
using TagKid.Lib.Utils;

namespace TagKid.Lib.Repositories.Impl
{
    public class CommentRepository : ICommentRepository
    {
        public IPage<CommentView> GetByPostId(long postId, int pageIndex, int pageSize)
        {
            return Db.SqlRepository().ExecuteQuery<CommentView>(Db.SqlBuilder()
                .SelectAllFrom<CommentView>()
                .Where()
                .Equals("post_id", postId)
                .OrderBy("id", true)
                .Build(),
                pageIndex, pageSize);
        }

        public void Save(Comment comment)
        {
            Db.SqlRepository().Save(comment);
        }
    }
}
