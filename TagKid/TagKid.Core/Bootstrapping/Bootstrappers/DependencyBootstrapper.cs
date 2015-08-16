using TagKid.Core.Domain;
using TagKid.Core.Domain.Impl;
using TagKid.Core.Providers;
using TagKid.Core.Providers.Impl;
using TagKid.Core.Repository;
using TagKid.Core.Repository.Impl;
using TagKid.Core.Service;
using TagKid.Core.Service.Impl;
using TagKid.Core.Service.Interceptors;
using TagKid.Framework.IoC;
using TagKid.Framework.Json;
using TagKid.Framework.Json.Newtonsoft;
using TagKid.Framework.UnitOfWork;
using TagKid.Framework.UnitOfWork.Impl;
using TagKid.Framework.WebApi;
using TagKid.Framework.WebApi.Impl;

namespace TagKid.Core.Bootstrapping.Bootstrappers
{
    class DependencyBootstrapper : IBootstrapper
    {
        public void Bootstrap(IDependencyContainer container)
        {
            container.RegisterSingleton<IJsonSerializer, NewtonsoftJsonSerializer>();
            container.RegisterSingleton<IRouteResolver, RouteResolver>();
            container.RegisterSingleton<IParameterResolver, ParameterResolver>();
            container.RegisterSingleton<IActionInvoker, ActionInvoker>();
            container.RegisterSingleton<IHttpHandler, HttpHandler>();
            container.RegisterSingleton<IAuthProvider, AuthProvider>();

            container.RegisterTransient<IUnitOfWork, UnitOfWork>();
            container.RegisterSingleton<IRepository, Framework.UnitOfWork.Impl.Repository>();
            container.RegisterSingleton<IAdoRepository, AdoRepository>();
            container.RegisterSingleton<IActionInterceptorBuilder, TagKidActionInterceptorBuilder>();

            container.RegisterSingleton<IPostService, PostService>();
            container.RegisterSingleton<IPostDomain, PostDomain>();

            container.RegisterSingleton<IAuthService, AuthService>();
            container.RegisterSingleton<IAuthDomain, AuthDomain>();

            container.RegisterSingleton<IPostRepository, PostRepository>();
            container.RegisterSingleton<IUserRepository, UserRepository>();
        }
    }
}