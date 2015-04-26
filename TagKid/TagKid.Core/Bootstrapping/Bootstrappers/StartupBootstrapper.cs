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
using TagKid.Framework.WebApi;
using TagKid.Framework.WebApi.Impl;

namespace TagKid.Core.Bootstrapping.Bootstrappers
{
    class StartupBootstrapper : IBootstrapper
    {
        public void Bootstrap(IDependencyContainer container)
        {
            container.RegisterSingleton<IJsonSerializer, NewtonsoftJsonSerializer>();
            container.RegisterSingleton<IRouteResolver, RouteResolver>();
            container.RegisterSingleton<IParameterResolver, ParameterResolver>();
            container.RegisterSingleton<IActionInvoker, ActionInvoker>();
            container.RegisterSingleton<IHttpHandler, HttpHandler>();

            container.RegisterTransient<IActionInterceptorBuilder, TagKidActionInterceptorBuilder>();

            container.RegisterTransient<IAuthProvider, AuthProvider>();

            container.RegisterTransient<IPostService, PostService>();
            container.RegisterTransient<IPostDomain, PostDomain>();
            container.RegisterTransient<IPostRepository, PostRepository>();
        }
    }
}