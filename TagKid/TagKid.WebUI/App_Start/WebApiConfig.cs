using System.Web.Http;
using TagKid.Core.Bootstrapping;
using TagKid.Framework.WebApi;

namespace TagKid.WebUI
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
                new GenericWebApiHandler(config)
                );
        }
    }
}
