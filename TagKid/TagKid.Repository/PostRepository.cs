using System;
using System.Collections.Generic;
using System.Linq;
using Taga.Core.Repository;
using TagKid.Core.Models.Database;
using TagKid.Core.Models.Filter;
using TagKid.Core.Repository;

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
            var posts = _repository.Select<Post>();
            var categories = _repository.Select<Category>();
            var users = _repository.Select<User>();

            var query = from p in posts
                        from u in users
                        from c in categories
                        where
                            p.Id == postId &&
                            p.CategoryId == c.Id &&
                            p.UserId == u.Id
                        select new { post = p, user = u, category = c };

            var queryResult = query.FirstOrDefault();

            if (queryResult == null)
            {
                return null;
            }

            var post = queryResult.post;
            post.User = queryResult.user;
            post.Category = queryResult.category;

            SetTags(post);
            SetRetaggedPosts(post);

            return post;
        }

        public Post[] GetForUserId(long userId, int maxCount, long maxPostId = 0)
        {
            var posts = _repository.Select<Post>();
            var categories = _repository.Select<Category>();
            var users = _repository.Select<User>();

            var query = from post in posts
                from user in users
                from category in categories
                where
                    post.UserId == user.Id &&
                    post.CategoryId == category.Id &&
                    category.UserId == user.Id
                orderby post.Id descending
                select new { post, category, user };

            var postList = new List<Post>();
            
            foreach (var item in query.ToList())
            {
                item.post.User = item.user;
                item.post.Category = item.category;
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

        public Post[] GetForUserId_(long userId, int maxCount, long maxPostId = 0)
        {
            var posts = _repository.Select<Post>();
            var categories = _repository.Select<Category>();
            var users = _repository.Select<User>();
            var followUsers = _repository.Select<FollowUser>();
            var followCategories = _repository.Select<FollowCategory>();

            var query = from p in posts
                        from u in users
                        from c in categories
                        from fu in followUsers
                        from fc in followCategories
                        where
                            ((fu.FollowerUserId == userId && fu.FollowedUserId == p.UserId) ||
                             (fc.UserId == userId && fc.CategoryId == p.CategoryId)) &&
                            p.CategoryId == c.Id &&
                            p.UserId == u.Id &&
                            p.Status == PostStatus.Published &&
                            p.AccessLevel != AccessLevel.Private &&
                            u.Status == UserStatus.Active &&
                            c.Status == CategoryStatus.Active &&
                            c.AccessLevel != AccessLevel.Private
                        orderby p.Id descending
                        select new { post = p, user = u, category = c };

            if (maxPostId > 0)
            {
                query = query.Where(q => q.post.Id < maxPostId);
            }

            var postList = new List<Post>();
            foreach (var item in query.Take(maxCount))
            {
                item.post.User = item.user;
                item.post.Category = item.category;
                postList.Add(item.post);
            }

            var result = postList.ToArray();

            SetTags(result);

            return result;
        }

        public IPage<Post> Search(PostFilter filter)
        {
            var posts = _repository.Select<Post>();
            var categories = _repository.Select<Category>();
            var users = _repository.Select<User>();

            var query = from p in posts
                        from c in categories
                        from u in users
                        where c.Id == p.CategoryId &&
                              p.UserId == u.Id &&
                              p.Status == PostStatus.Published &&
                              p.AccessLevel == AccessLevel.Public &&
                              c.Status == CategoryStatus.Active &&
                              c.AccessLevel == AccessLevel.Public &&
                              u.Status == UserStatus.Active
                        orderby p.Id descending
                        select new { post = p, user = u, category = c };

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
                item.post.Category = item.category;
                item.post.User = item.user;
                postList.Add(item.post);
            }

            var postArr = postList.ToArray();

            SetTags(postArr);
            SetRetaggedPosts(postArr);

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
                _repository.Delete<PostTag>(pt => pt.PostId, post.Id);
            }

            _repository.Save(post);

            foreach (var tag in post.Tags.Where(tag => tag.Id < 1))
            {
                tag.Status = TagStatus.Active;
                tag.Count = 1;
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

        private void SetRetaggedPosts(params Post[] posts)
        {
            var retags = posts
                .Where(post => post.RetaggedPostId > 0)
                .ToList();

            if (!retags.Any())
            {
                return;
            }
            
            var retaggedPostIds = posts
                .Select(post => post.RetaggedPostId)
                .Distinct()
                .ToList();

            var allPosts = _repository.Select<Post>();
            var users = _repository.Select<User>();
            var categories = _repository.Select<Category>();

            // TODO: access level of retagged post??? 

            var query = from p in allPosts
                        from u in users
                        from c in categories
                        where
                            retaggedPostIds.Contains(p.Id) &&
                            p.UserId == u.Id &&
                            p.CategoryId == c.Id &&
                            p.AccessLevel != AccessLevel.Private &&
                            c.AccessLevel != AccessLevel.Private &&
                            p.Status == PostStatus.Published &&
                            c.Status == CategoryStatus.Active &&
                            u.Status == UserStatus.Active
                        select new { post = p, user = u, category = c };

            var retaggedPosts = new List<Post>();

            foreach (var item in query)
            {
                item.post.User = item.user;
                item.post.Category = item.category;
                retaggedPosts.Add(item.post);
            }

            SetTags(retaggedPosts.ToArray());

            foreach (var post in retags)
            {
                foreach (var retaggedPost in retaggedPosts)
                {
                    if (post.RetaggedPostId == retaggedPost.Id)
                    {
                        post.RetaggedPost = retaggedPost;
                    }
                }
            }
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
    }
}