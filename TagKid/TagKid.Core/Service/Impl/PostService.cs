using TagKid.Core.Domain;
using TagKid.Core.Models.Database;
using TagKid.Core.Models.Messages.Post;
using TagKid.Framework.WebApi;

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
        
        public Response Dummy(DummyRequest request)
        {
            return Response.Success.WithData(new User
            {
                Id = request.Id,
                Email = "taga@mail.com"
            });
        }
    }
}
