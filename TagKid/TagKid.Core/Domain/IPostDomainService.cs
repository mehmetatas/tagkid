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

        PostDO[] GetPostsOfUser(long userId, long maxPostId);

        IPage<Post> SearchByTag(long tagId, int pageIndex);

        Post[] ContinueSearchByTag(long tagId, long maxPostId);

        Tag[] SearchTags(string name);

        Comment[] GetComments(long postId, long maxCommentId = 0);

        LikeResultDO LikeUnlike(long postId);
    }
}
