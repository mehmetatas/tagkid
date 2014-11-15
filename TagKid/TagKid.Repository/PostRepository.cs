using System;
using System.Linq;
using Taga.Core.Repository;
using TagKid.Core.Models.Database;
using TagKid.Core.Models.Filter;
using TagKid.Core.Repositories;

namespace TagKid.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly IRepository _repository;

        public PostRepository(IRepository repository)
        {
            _repository = repository;
        }

        public Post GetById(long postId)
        {
            return _repository.Select<Post>()
                .FirstOrDefault(p => p.Id == postId);
        }

        public IPage<Post> GetForUserId(long userId, int maxCount, long maxPostId = 0)
        {
            throw new NotImplementedException();
        }

        public IPage<Post> Search(PostFilter filter)
        {
            var posts = _repository.Select<Post>();
            var categories = _repository.Select<Category>();
            var users = _repository.Select<User>();

            var query = from post in posts
                        from cat in categories
                        from user in users
                        where cat.Id == post.CategoryId &&
                              post.UserId == user.Id &&
                              post.Status == PostStatus.Published &&
                              cat.Status == CategoryStatus.Active &&
                              user.Status == UserStatus.Active
                        orderby post.Id descending
                        select new { post, cat, user };

            if (filter.ByTitle)
            {
                query = query.Where(a => a.post.Title.Contains(filter.Title));
            }

            if (!filter.ByTag)
            {
                return query.Select(a => a.post)
                    .Distinct()
                    .Page(filter.PageIndex, filter.PageSize);
            }

            var postTags = _repository.Select<PostTag>();

            var queryWithTagFilter = from q in query
                                     from pt in postTags
                                     where pt.PostId == q.post.Id &&
                                           filter.TagIds.Contains(pt.TagId)
                                     select q.post;

            return queryWithTagFilter.Distinct()
                .Page(filter.PageIndex, filter.PageSize);
        }

        public void Save(Post post)
        {
            if (post.Id > 0)
            {
                _repository.Delete<PostTag>(pt => pt.PostId, post.Id);
            }

            _repository.Save(post);

            foreach (var tag in post.Tags.Where(tag => tag.Id < 1))
            {
                _repository.Insert(tag);
            }

            _repository.Flush();

            foreach (var tag in post.Tags)
            {
                _repository.Insert(new PostTag
                {
                    PostId = post.Id,
                    TagId = tag.Id
                });
            }
        }

        public void Save(PostLike postLike)
        {
            _repository.Insert(postLike);
        }

        public void Delete(PostLike postLike)
        {
            _repository.Delete(postLike);
        }

        public int GetLikeCount(long postId)
        {
            return _repository.Select<PostLike>()
                .Count(pl => pl.PostId == postId);
        }

        public User[] GetLikers(long postId, int maxCount, DateTime maxLikeDate)
        {
            var likes = _repository.Select<PostLike>();
            var users = _repository.Select<User>();

            return (from like in likes
                    from user in users
                    where like.PostId == postId &&
                          like.UserId == user.Id &&
                          like.LikedDate < maxLikeDate
                    orderby like.LikedDate descending 
                    select user)
                .Take(maxCount)
                .ToArray();
        }
    }
}