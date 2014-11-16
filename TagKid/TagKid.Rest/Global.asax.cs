using System.Web.Http;
using TagKid.Application.Bootstrapping;

namespace TagKid.Rest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Bootstrapper.StartApp();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}