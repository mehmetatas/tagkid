using TagKid.Lib.Models.DTO.Messages;

namespace TagKid.Lib.Services
{
    public interface IPostService
    {
        PostResponse SavePost(PostRequest postRequest);
    }
}