using TagKid.Core.Models.DTO.Messages;
using TagKid.Core.Models.DTO.Messages.Post;

namespace TagKid.Core.Service
{
    public interface IPostService
    {
        Response GetTimeline(GetTimelineRequest request);

        Response SavePost(SavePostRequest request);

        Response CreateCategory(CreateCategoryRequest request);

        Response GetCategories();
    }
}