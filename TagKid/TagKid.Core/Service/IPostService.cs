using TagKid.Core.Models.Messages;
using TagKid.Core.Models.Messages.Post;

namespace TagKid.Core.Service
{
    public interface IPostService
    {
        Response Save(SaveRequest request);
    }
}
