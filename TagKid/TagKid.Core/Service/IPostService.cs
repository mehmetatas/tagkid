using TagKid.Core.Models.Messages.Post;
using TagKid.Framework.WebApi;

namespace TagKid.Core.Service
{
    public interface IPostService
    {
        Response Save(SaveRequest request);
        Response Dummy(DummyRequest request);
    }

    public class DummyRequest
    {
        public long Id { get; set; }
    }
}
