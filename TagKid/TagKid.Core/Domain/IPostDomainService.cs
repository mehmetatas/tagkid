using Taga.Core.Repository;
using TagKid.Core.Models.Database;
using TagKid.Core.Models.Domain;

namespace TagKid.Core.Domain
{
    public interface IPostDomainService : ITagKidDomainService
    {
        void SaveAsDraft(Post post);

        void Publish(Post post);

        Post Get(long postId);

        PostDO[] GetTimeline(long maxPostId = 0);

        IPage<Post> SearchByTag(long tagId, int pageIndex);

        Post[] ContinueSearchByTag(long tagId, long maxPostId);

        IPage<Post> GetPostsOfUser(long userId, int pageIndex);

        Post[] ContinuteGetPostsOfUser(long userId, long maxPostId);

        IPage<Post> GetPostsOfCategory(long categoryId, int pageIndex);

        Post[] ContinueGetPostsOfCategory(long categoryId, long maxPostId);

        Tag[] SearchTags(string name);

        Category[] GetCategoriesOfUser(long userId);

        void CreateCategory(Category category);

        Comment[] GetComments(long postId, long maxCommentId = 0);

        LikeResultDO LikeUnlike(long postId);
    }
}
