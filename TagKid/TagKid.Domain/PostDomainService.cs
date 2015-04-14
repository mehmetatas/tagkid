using System;
using System.Linq;
using Taga.Core.IoC;
using Taga.Core.Repository;
using TagKid.Core.Domain;
using TagKid.Core.Exceptions;
using TagKid.Core.Models;
using TagKid.Core.Models.Database;
using TagKid.Core.Models.Database.View;
using TagKid.Core.Models.Domain;
using TagKid.Core.Repository;
using TagKid.Core.Utils;

namespace TagKid.Domain
{
    public class PostDomainService : IPostDomainService
    {
        private const int PageSize = 10;

        private static IUserRepository UserRepository
        {
            get { return ServiceProvider.Provider.GetOrCreate<IUserRepository>(); }
        }

        private static ITagRepository TagRepository
        {
            get { return ServiceProvider.Provider.GetOrCreate<ITagRepository>(); }
        }

        private static IPostRepository PostRepository
        {
            get { return ServiceProvider.Provider.GetOrCreate<IPostRepository>(); }
        }

        private static ICommentRepository CommentRepository
        {
            get { return ServiceProvider.Provider.GetOrCreate<ICommentRepository>(); }
        }

        public virtual PostDO Save(Post post)
        {
            if (post.Id > 0)
            {
                var tmp = PostRepository.GetById(post.Id);

                if (tmp.UserId != RequestContext.User.Id)
                {
                    throw Errors.S_InvalidUserForPostEdit.ToException();
                }

                tmp.Title = post.Title;
                tmp.Tags = post.Tags;
                tmp.HtmlContent = post.HtmlContent;
                tmp.AccessLevel = post.AccessLevel;
                tmp.UpdateDate = DateTime.UtcNow;

                post = tmp;
            }
            else
            {
                post.CreateDate = DateTime.Now;
            }

            if (!post.PublishDate.HasValue && post.AccessLevel == AccessLevel.Public)
            {
                post.PublishDate = DateTime.UtcNow;
            }

            post.User = RequestContext.User;
            post.UserId = post.User.Id;

            PostRepository.Save(post);

            var info = PostRepository.GetPostInfo(post.UserId, new[] { post.Id })
                .FirstOrDefault();

            return new PostDO(post, info);
        }

        public virtual PostDO Get(long postId)
        {
            var post = PostRepository.GetById(postId);
            var info = PostRepository.GetPostInfo(RequestContext.User.Id, new[] { postId });
            return new PostDO(post, info[0]);
        }

        public virtual PostDO[] GetTimeline(long maxPostId = 0)
        {
            var posts = PostRepository.GetForUserId(RequestContext.User.Id, PageSize, maxPostId);

            var infos = posts.Any()
                ? PostRepository.GetPostInfo(RequestContext.User.Id, posts.Select(p => p.Id).ToArray())
                : new PostInfo[0];

            return PostDO.Create(posts, infos);
        }

        public virtual PostDO[] GetAnonymousTimeline()
        {
            // Ten most popular posts of last 30 days
            var posts = PostRepository.GetPopularPosts(30, 10);

            var infos = posts.Any()
                ? PostRepository.GetPostInfo(-1, posts.Select(p => p.Id).ToArray())
                : new PostInfo[0];

            return PostDO.Create(posts, infos);
        }

        public virtual PostDO[] GetPostsOfUser(long userId, long maxPostId)
        {
            var posts = PostRepository.GetPostsOfUser(userId, PageSize, maxPostId);

            var infos = posts.Any()
                ? PostRepository.GetPostInfo(RequestContext.IsAuthenticated ? RequestContext.User.Id : -1, posts.Select(p => p.Id).ToArray())
                : new PostInfo[0];

            return PostDO.Create(posts, infos);
        }

        public virtual IPage<Post> SearchByTag(long tagId, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public virtual Post[] ContinueSearchByTag(long tagId, long maxPostId)
        {
            throw new NotImplementedException();
        }

        public virtual IPage<Post> GetPostsOfCategory(long categoryId, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public virtual Post[] ContinueGetPostsOfCategory(long categoryId, long maxPostId)
        {
            throw new NotImplementedException();
        }

        public virtual Tag[] SearchTags(string name)
        {
            return TagRepository.Search(name, 1, 10).Items.ToArray();
        }

        public Comment[] GetComments(long postId, long maxCommentId = 0)
        {
            return CommentRepository.GetComments(postId, PageSize, maxCommentId);
        }

        public LikeResultDO LikeUnlike(long postId)
        {
            var userId = RequestContext.User.Id;

            var liked = PostRepository.HasLiked(postId, userId);
            var likeCount = PostRepository.GetLikeCount(postId);

            if (liked)
            {
                likeCount--;
                PostRepository.Unlike(postId, userId);
            }
            else
            {
                likeCount++;
                PostRepository.Like(postId, userId);
            }

            return new LikeResultDO
            {
                Liked = !liked,
                LikeCount = likeCount
            };
        }

        public Comment Comment(long postId, string content)
        {
            var comment = new Comment
            {
                Content = content.Replace("\n", "<br>"),
                PostId = postId,
                PublishDate = DateTime.UtcNow,
                UserId = RequestContext.User.Id,
                User = RequestContext.User,
                Status = CommentStatus.Active
            };

            CommentRepository.Save(comment);

            return comment;
        }
    }
}
