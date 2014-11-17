using Taga.Core.Repository;
using TagKid.Core.Models.Database;
using TagKid.Core.Models.Domain;

namespace TagKid.Core.Domain
{
    public interface IPostDomainService : ITagKidDomainService
    {
        PostProxy Get(long postId);

        PostProxy[] GetPostsForWall(int pageSize, long maxPostId = 0);

        IPage<PostProxy> SearchByTag(long tagId, int pageIndex, int pageSize);

        PostProxy[] ContinueSearchByTag(long tagId, long maxPostId, int pageSize);

        IPage<PostProxy> GetPostsOfUser(long userId, int pageIndex, int pageSize);

        PostProxy[] ContinuteGetPostsOfUser(long userId, long maxPostId, int pageSize);

        IPage<PostProxy> GetPostsOfCategory(long categoryId, int pageIndex, int pageSize);

        PostProxy[] ContinueGetPostsOfCategory(long categoryId, long maxPostId, int pageSize);

        Tag[] SearchTags(string name, int pageSize);
    }
}
