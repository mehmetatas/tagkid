using System.Web.Http;
using System.Web.Routing;
using Taga.Core.IoC;
using Taga.Core.Mapping;
using TagKid.Lib.Bootstrapping;
using TagKid.Lib.NHibernate;
using TagKid.Lib.Repositories;
using TagKid.Lib.Repositories.Impl;
using TagKid.Lib.Services;
using TagKid.Lib.Services.Impl;
using TagKid.Lib.Utils;

namespace TagKid.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Bootstrapper.StartApp();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
