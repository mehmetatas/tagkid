using TagKid.Core.Models.Messages;
using TagKid.Core.Models.Messages.Post;

namespace TagKid.Core.Service.Impl
{
    public class PostService : IPostService
    {
        public Response Save(SaveRequest request)
        {
            return Response.Success;
        }
    }
}
