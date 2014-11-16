using TagKid.Core.Models.DTO.Messages;

namespace TagKid.Core.Service
{
    public interface IPostService
    {
        PostResponse SavePost(PostRequest postRequest);
    }
}