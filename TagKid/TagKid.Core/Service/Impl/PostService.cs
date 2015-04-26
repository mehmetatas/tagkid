using TagKid.Core.Domain;
using TagKid.Core.Models.Messages;
using TagKid.Core.Models.Messages.Post;

namespace TagKid.Core.Service.Impl
{
    public class PostService : IPostService
    {
        private readonly IPostDomain _domain;

        public PostService(IPostDomain domain)
        {
            _domain = domain;
        }

        public Response Save(SaveRequest request)
        {
            _domain.Save(request.Post);
            return Response.Success;
        }
    }
}
