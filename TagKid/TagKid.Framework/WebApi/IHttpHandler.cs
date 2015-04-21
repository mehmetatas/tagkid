using System.Net.Http;

namespace TagKid.Framework.WebApi
{
    public interface IHttpHandler
    {
        void Handle(HttpRequestMessage request, HttpResponseMessage response);
    }
}
