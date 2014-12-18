using System;
using System.Linq;
using Taga.Core.IoC;
using Taga.Core.Repository;
using TagKid.Core.Domain;
using TagKid.Core.Models;
using TagKid.Core.Models.Database;
using TagKid.Core.Repository;
using TagKid.Core.Utils;

namespace TagKid.Domain
{
    public class PostDomainService : IPostDomainService
    {
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

        public virtual void SaveAsDraft(Post post)
        {
            post.UserId = RequestContext.Current.AuthToken.UserId;
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

        public virtual Post Get(long postId)
        {
            throw new NotImplementedException();
        }

        public virtual Post[] GetTimeline(int pageSize, long maxPostId = 0)
        {
            return PostRepository.GetForUserId(1L, pageSize);
        }

        public virtual IPage<Post> SearchByTag(long tagId, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public virtual Post[] ContinueSearchByTag(long tagId, long maxPostId, int pageSize)
        {
            throw new NotImplementedException();
        }

        public virtual IPage<Post> GetPostsOfUser(long userId, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public virtual Post[] ContinuteGetPostsOfUser(long userId, long maxPostId, int pageSize)
        {
            throw new NotImplementedException();
        }

        public virtual IPage<Post> GetPostsOfCategory(long categoryId, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public virtual Post[] ContinueGetPostsOfCategory(long categoryId, long maxPostId, int pageSize)
        {
            throw new NotImplementedException();
        }

        public virtual Tag[] SearchTags(string name, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
