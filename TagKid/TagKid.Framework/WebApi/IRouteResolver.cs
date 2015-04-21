using System.Net.Http;

namespace TagKid.Framework.WebApi
{
    public interface IRouteResolver
    {
        RouteContext Resolve(HttpRequestMessage request);
    }
}
