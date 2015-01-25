using TagKid.Core.Models.DTO.Messages;
using TagKid.Core.Models.DTO.Messages.Post;

namespace TagKid.Core.Service
{
    public interface IPostService
    {
        Response GetTimeline(GetTimelineRequest request);

        Response GetPosts(GetPostsRequest request);

        Response SaveAsDraft(SaveAsDraftRequest request);

        Response Publish(PublishRequest request);

        Response CreateCategory(CreateCategoryRequest request);

        Response GetCategories(GetCategoriesRequest request);

        Response GetComments(GetCommentsRequest request);

        Response LikeUnlike(LikeUnlikeRequest request);
    }
}