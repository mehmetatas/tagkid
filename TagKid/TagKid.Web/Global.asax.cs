using System.Web;
using System.Web.Http;
using System.Web.Routing;
using TagKid.Lib.Bootstrapping;

namespace TagKid.Web
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Bootstrapper.StartApp();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}