using Taga.Core.DynamicProxy;
using TagKid.Core.Models.DTO.Messages;
using TagKid.Core.Models.DTO.Messages.Post;
using TagKid.Core.Service;

namespace TagKid.Service
{
    [Intercept]
    public class PostService : IPostService
    {
        public Response GetTimeline(GetTimelineRequest request)
        {
            throw new System.NotImplementedException();
        }

        public Response SavePost(SavePostRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}