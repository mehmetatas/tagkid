using System.Linq;
using Taga.Core.Repository;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Models.Entities.Views;

namespace TagKid.Lib.Repositories.Impl
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IRepository _repository;

        public CommentRepository(IRepository repository)
        {
            _repository = repository;
        }

        public IPage<CommentView> GetByPostId(long postId, int pageIndex, int pageSize)
        {
            return _repository.Query<CommentView>()
                .Where(cv => cv.PostId == postId)
                .OrderByDescending(cv => cv.Id)
                .Page(pageIndex, pageSize);
        }

        public void Save(Comment comment)
        {
            _repository.Save(comment);
        }
    }
}