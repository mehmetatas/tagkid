using System.Linq;
using Taga.Core.Repository;
using TagKid.Core.Models.Database;
using TagKid.Core.Repository;

namespace TagKid.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IRepository _repository;

        public CommentRepository(IRepository repository)
        {
            _repository = repository;
        }

        public Comment Get(long commentId)
        {
            return _repository.Select<Comment>()
              .FirstOrDefault(c => c.Id == commentId && c.Status == CommentStatus.Active);
        }

        public int GetCommentCount(long postId)
        {
            return _repository.Select<Comment>()
                .Count(c => c.PostId == postId && c.Status == CommentStatus.Active);
        }

        public Comment[] GetComments(long postId, int maxCount, long maxCommentId = 0)
        {
            var query = _repository.Select<Comment>()
                .Where(c => c.PostId == postId && c.Status == CommentStatus.Active);

            if (maxCommentId > 0)
            {
                query = query.Where(c => c.Id < maxCommentId);
            }

            var comments = query.OrderByDescending(c => c.Id)
                .Take(maxCount)
                .ToArray();

            SetUsers(comments);

            return comments;
        }

        public void Save(Comment comment)
        {
            _repository.Save(comment);
        }

        private void SetUsers(Comment[] comments)
        {
            if (comments.Length == 0)
            {
                return;
            }

            var userIds = comments.Select(c => c.UserId).Distinct();

            var users = _repository.Select<User>()
                .Where(u => userIds.Contains(u.Id))
                .ToArray();

            foreach (var comment in comments)
            {
                comment.User = users.First(u => u.Id == comment.UserId);
            }
        }

        public void Flush()
        {
            _repository.Flush();
        }
    }
}