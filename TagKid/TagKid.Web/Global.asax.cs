using System.Web.Http;
using Taga.Core.IoC;
using Taga.Core.Repository;
using TagKid.Lib.PetaPoco.Repository;
using TagKid.Lib.Services;
using TagKid.Lib.Services.Impl;

namespace TagKid.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RegisterServices();

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        private void RegisterServices()
        {
            var prov = ServiceProvider.Provider;

            prov.RegisterProxy<IAuthService, AuthService>();
            prov.RegisterProxy<IRepository, PetaPocoRepository>();
            prov.RegisterProxy<IUnitOfWork, PetaPocoUnitOfWork>();
            prov.RegisterProxy<ISqlBuilder, PetaPocoSqlBuilder>();
        }
    }
}
