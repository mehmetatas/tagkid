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
        private const int PageSize = 1;

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

        private static ICategoryRepository CategoryRepository
        {
            get { return ServiceProvider.Provider.GetOrCreate<ICategoryRepository>(); }
        }

        private static ICommentRepository CommentRepository
        {
            get { return ServiceProvider.Provider.GetOrCreate<ICommentRepository>(); }
        }

        public virtual void SaveAsDraft(Post post)
        {
            post.UserId = RequestContext.User.Id;
            post.HtmlContent = EditorUtils.ToHtml(post.EditorContent);
            post.Status = PostStatus.Draft;
            post.AccessLevel = AccessLevel.Private;
            post.CategoryId = post.Category.Id;
            post.RetaggedPostId = post.RetaggedPost == null
                ? (long?)null
                : post.RetaggedPost.Id;

            if (post.CreateDate == default(DateTime))
            {
                post.CreateDate = DateTime.Now;
            }

            PostRepository.Save(post);
        }

        public void Publish(Post post)
        {
            post.UserId = RequestContext.User.Id;
            post.HtmlContent = EditorUtils.ToHtml(post.EditorContent);
            post.Status = PostStatus.Published;
            post.AccessLevel = AccessLevel.Private;
            post.CategoryId = post.Category.Id;
            post.RetaggedPostId = post.RetaggedPost == null
                ? (long?)null
                : post.RetaggedPost.Id;

            if (post.CreateDate == default(DateTime))
            {
                post.CreateDate = DateTime.Now;
            }

            post.PublishDate = DateTime.Now;

            PostRepository.Save(post);
        }

        public virtual Post Get(long postId)
        {
            throw new NotImplementedException();
        }

        public virtual PostDO[] GetTimeline(long maxPostId = 0)
        {
            var posts = PostRepository.GetForUserId(RequestContext.User.Id, PageSize, maxPostId);

            var infos = posts.Any()
                ? PostRepository.GetPostInfo(RequestContext.User.Id, posts.Select(p => p.Id).ToArray()) 
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

        public virtual IPage<Post> GetPostsOfUser(long userId, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public virtual Post[] ContinuteGetPostsOfUser(long userId, long maxPostId)
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
            throw new NotImplementedException();
        }

        public Category[] GetCategoriesOfUser(long userId)
        {
            return CategoryRepository.GetCategories(userId);
        }

        public void CreateCategory(Category category)
        {
            category.UserId = RequestContext.User.Id;
            category.Status = CategoryStatus.Active;
            CategoryRepository.Save(category);
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
    }
}
