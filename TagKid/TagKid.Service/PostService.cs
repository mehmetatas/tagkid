using Taga.Core.DynamicProxy;
using TagKid.Core.Domain;
using TagKid.Core.Models.DTO.Messages;
using TagKid.Core.Models.DTO.Messages.Post;
using TagKid.Core.Service;

namespace TagKid.Service
{
    [Intercept]
    public class PostService : IPostService
    {
        private readonly IPostDomainService _postDomain;

        public PostService(IPostDomainService postDomain)
        {
            _postDomain = postDomain;
        }

        public virtual Response GetTimeline(GetTimelineRequest request)
        {
            var posts = _postDomain.GetTimeline(10);
            return Response.Success.WithData(posts);
        }

        public virtual Response SavePost(SavePostRequest request)
        {
            _postDomain.SaveAsDraft(request.Post);
            return Response.Success.WithData(request.Post);
        }
    }
}