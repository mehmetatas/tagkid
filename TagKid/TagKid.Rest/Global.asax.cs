using System.Web.Http;
using Taga.Core.Rest;
using TagKid.Application.Bootstrapping;

namespace TagKid.Rest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Bootstrapper.StartApp();

            var cfg = GlobalConfiguration.Configuration;

            cfg.Routes.MapHttpRoute(
                "GenericHttpApi",
                "api/{*.}",
                new { controller = "GenericHttpApi" },
                null,
                new GenericApiHandler(cfg)
                );
        }
    }
}