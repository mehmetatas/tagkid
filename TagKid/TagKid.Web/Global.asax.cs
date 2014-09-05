using System.Web.Http;
using Taga.Core.IoC;
using Taga.Core.Repository;
using Taga.Core.Repository.Sql;
using TagKid.Lib.PetaPoco.Repository;
using TagKid.Lib.PetaPoco.Repository.Sql;
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

            prov.Register<IAuthService, AuthService>();
            prov.Register<ISqlRepository, PetaPocoSqlRepository>();
            prov.Register<IUnitOfWork, PetaPocoUnitOfWork>();
            prov.Register<ISqlBuilder, PetaPocoSqlBuilder>();
        }
    }
}
