using System.Web.Http;
using Taga.Core.Rest;

namespace TagKid.Rest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                     "GenericHttpApi",
                     "api/{*.}",
                     new { controller = "GenericHttpApi" },
                     null,
                     new GenericApiHandler(config)
                );
        }
    }
}
