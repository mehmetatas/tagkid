using Taga.Core.Repository;
using TagKid.Core.Models.Database;

namespace TagKid.Core.Domain
{
    public interface IPostDomainService : ITagKidDomainService
    {
        void SaveAsDraft(Post post);

        Post Get(long postId);

        Post[] GetTimeline(int pageSize, long maxPostId = 0);

        IPage<Post> SearchByTag(long tagId, int pageIndex, int pageSize);

        Post[] ContinueSearchByTag(long tagId, long maxPostId, int pageSize);

        IPage<Post> GetPostsOfUser(long userId, int pageIndex, int pageSize);

        Post[] ContinuteGetPostsOfUser(long userId, long maxPostId, int pageSize);

        IPage<Post> GetPostsOfCategory(long categoryId, int pageIndex, int pageSize);

        Post[] ContinueGetPostsOfCategory(long categoryId, long maxPostId, int pageSize);

        Tag[] SearchTags(string name, int pageSize);

        Category[] GetCategoriesOfUser(long userId);

        void CreateCategory(Category category);
    }
}
