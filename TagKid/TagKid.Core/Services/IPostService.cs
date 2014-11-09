using TagKid.Core.Models.DTO.Messages;

namespace TagKid.Core.Services
{
    public interface IPostService
    {
        PostResponse SavePost(PostRequest postRequest);
    }
}