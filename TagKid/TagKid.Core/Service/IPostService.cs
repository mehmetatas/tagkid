using TagKid.Core.Models.DTO.Messages;
using TagKid.Core.Models.DTO.Messages.Post;

namespace TagKid.Core.Service
{
    public interface IPostService
    {
        Response GetTimeline(GetTimelineRequest request);

        Response GetPosts(GetPostsRequest request);

        Response Save(SaveRequest request);

        Response GetComments(GetCommentsRequest request);

        Response LikeUnlike(LikeUnlikeRequest request);
        Response SearchTags(SearchTagsRequest request);
    }
}