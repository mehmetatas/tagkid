using System.Threading.Tasks;

namespace TagKid.Framework.Hosting
{
    public interface IHttpRequestHandler
    {
        Task Handle(IHttpRequest httpRequest, IHttpResponse httpResponse);
    }
}
