using System;
using System.Collections.Generic;
using System.Linq;
using Taga.Core.Repository;
using TagKid.Core.Models.Database;
using TagKid.Core.Models.Database.View;
using TagKid.Core.Models.Filter;
using TagKid.Core.Repository;

namespace TagKid.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly IRepository _repository;
        private readonly ISqlRepository _sqlRepository;

        public PostRepository(IRepository repository, ISqlRepository sqlRepository)
        {
            _repository = repository;
            _sqlRepository = sqlRepository;
        }

        public Post GetById(long postId)
        {
            var posts = _repository.Select<Post>();
            var users = _repository.Select<User>();

            var query = from p in posts
                        from u in users
                        where
                            p.Id == postId &&
                            p.UserId == u.Id
                        select new { post = p, user = u };

            var queryResult = query.FirstOrDefault();

            if (queryResult == null)
            {
                return null;
            }

            var post = queryResult.post;
            post.User = queryResult.user;

            SetTags(post);

            return post;
        }

        public Post[] GetForUserId(long userId, int maxCount, long maxPostId = 0)
        {
            var posts = _repository.Select<Post>();
            var users = _repository.Select<User>();

            var query = from post in posts
                        from user in users
                        where
                            post.UserId == user.Id
                        orderby post.Id descending
                        select new { post, user };

            if (maxPostId > 0)
            {
                query = query.Where(q => q.post.Id < maxPostId);
            }

            var postList = new List<Post>();

            foreach (var item in query.Take(maxCount))
            {
                item.post.User = item.user;
                if (item.post.PublishDate == null)
                {
                    item.post.PublishDate = item.post.CreateDate;
                }
                postList.Add(item.post);
            }

            var postArr = postList.ToArray();

            SetTags(postArr);

            return postArr;
        }

        public Post[] GetPostsOfUser(long userId, int maxCount, long maxPostId)
        {
            var posts = _repository.Select<Post>();
            var users = _repository.Select<User>();

            var query = from post in posts
                        from user in users
                        where
                            user.Id == userId &&
                            post.UserId == user.Id
                        orderby post.Id descending
                        select new { post, user };

            if (maxPostId > 0)
            {
                query = query.Where(q => q.post.Id < maxPostId);
            }

            var postList = new List<Post>();

            foreach (var item in query.Take(maxCount))
            {
                item.post.User = item.user;
                if (item.post.PublishDate == null)
                {
                    item.post.PublishDate = item.post.CreateDate;
                }
                postList.Add(item.post);
            }

            var postArr = postList.ToArray();

            SetTags(postArr);

            return postArr;
        }

        public PostInfo[] GetPostInfo(long userId, params long[] postIds)
        {
            var sql = @"select 
	ISNULL(Likes.PostId, Comments.PostId) as PostId, 
	ISNULL(Likes.LikeCount, 0) as LikeCount,
	(select count(*) from PostLike where PostId = Likes.PostId and UserId = ~p_userId) as Liked,
	ISNULL(Comments.CommentCount, 0) as CommentCount
	--,(select count(*) from Comment where PostId = Comments.PostId and UserId = ~p_userId) as Commented
from 
	(select PostId, count(PostId) as LikeCount from PostLike where PostId in ({0}) group by PostId) Likes
	full join (select PostId, count(PostId) as CommentCount from Comment where PostId in ({0}) group by PostId) Comments
on
	Likes.PostId = Comments.PostId";

            var postIdsParamsStr = Enumerable.Range(0, postIds.Length).Select(i => String.Format("~p_{0}", i));

            sql = string.Format(sql, String.Join(",", postIdsParamsStr));

            var parameters = new Dictionary<string, object>();
            parameters.Add("p_userId", userId);

            for (var i = 0; i < postIds.Length; i++)
            {
                parameters.Add(String.Format("p_{0}", i), postIds[i]);
            }

            return _sqlRepository.Query<PostInfo>(sql, parameters, true).ToArray();
        }

        public IPage<Post> Search(PostFilter filter)
        {
            var posts = _repository.Select<Post>();
            var users = _repository.Select<User>();

            var query = from p in posts
                        from u in users
                        where p.UserId == u.Id &&
                              p.Status == PostStatus.Published &&
                              p.AccessLevel == AccessLevel.Public &&
                              u.Status == UserStatus.Active
                        orderby p.Id descending
                        select new { post = p, user = u };

            if (filter.ByTitle)
            {
                query = query.Where(a => a.post.Title.Contains(filter.Title));
            }

            if (filter.ByTag)
            {
                var postTags = _repository.Select<PostTag>();

                query = from q in query
                        from pt in postTags
                        where pt.PostId == q.post.Id &&
                              filter.TagIds.Contains(pt.TagId)
                        select q;
            }

            var page = query.Page(filter.PageIndex, filter.PageSize);

            var postList = new List<Post>();

            foreach (var item in page.Items)
            {
                item.post.User = item.user;
                postList.Add(item.post);
            }

            var postArr = postList.ToArray();

            SetTags(postArr);

            return new Page<Post>
            {
                CurrentPage = page.CurrentPage,
                PageSize = page.PageSize,
                TotalCount = page.TotalCount,
                TotalPages = page.TotalPages,
                Items = postArr
            };
        }

        public void Save(Post post)
        {
            if (post.Id > 0)
            {
                _sqlRepository.Delete<PostTag>(pt => pt.PostId, post.Id);
            }

            _repository.Save(post);

            var tagNames = post.Tags.Select(t => t.Name).ToArray();

            var existingTags = _repository.Select<Tag>()
                .Where(t => tagNames.Contains(t.Name))
                .ToList();

            foreach (var tag in post.Tags)
            {
                var existingTag = existingTags.FirstOrDefault(t => t.Name == tag.Name);
                if (existingTag == null)
                {
                    tag.Status = TagStatus.Active;
                    tag.Count = 1;
                    _repository.Insert(tag);
                }
                else
                {
                    tag.Id = existingTag.Id;
                    existingTag.Count += 1;
                    _repository.Update(existingTag);
                }
            }

            foreach (var tag in post.Tags)
            {
                _repository.Insert(new PostTag
                {
                    PostId = post.Id,
                    TagId = tag.Id
                });
            }
        }

        public int GetPostCount(long userId)
        {
            return _repository.Select<Post>()
                .Count(post => post.UserId == userId && post.Status == PostStatus.Published);
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

        private void SetTags(params Post[] posts)
        {
            var postTags = _repository.Select<PostTag>();
            var tags = _repository.Select<Tag>();
            var postIds = posts.Select(post => post.Id).ToList();

            var query = from postTag in postTags
                        from tag in tags
                        where
                            postIds.Contains(postTag.PostId) &&
                            postTag.TagId == tag.Id &&
                            tag.Status == TagStatus.Active
                        orderby postTag.PostId
                        select new { postTag.PostId, Tag = tag };

            Post currPost = null;
            foreach (var item in query)
            {
                if (currPost == null || currPost.Id != item.PostId)
                {
                    currPost = posts.First(post => post.Id == item.PostId);
                    currPost.Tags = new List<Tag>();
                }
                currPost.Tags.Add(item.Tag);
            }
        }

        public void Flush()
        {
            _repository.Flush();
        }

        public void Like(long postId, long userId)
        {
            _repository.Insert(new PostLike
            {
                LikedDate = DateTime.Now,
                PostId = postId,
                UserId = userId
            });
        }

        public void Unlike(long postId, long userId)
        {
            _repository.Delete(new PostLike
            {
                PostId = postId,
                UserId = userId
            });
        }

        public bool HasLiked(long postId, long userId)
        {
            return _repository.Select<PostLike>()
                .Any(pl => pl.PostId == postId && pl.UserId == userId);
        }
    }
}