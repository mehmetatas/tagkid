using System.Web.Http;
using Taga.Core.Rest;
using TagKid.Application.Bootstrapping;

namespace TagKid.WebUI2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            Bootstrapper.StartApp();

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
