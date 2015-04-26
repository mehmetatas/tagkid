using TagKid.Framework.IoC;
using TagKid.Framework.Json;
using TagKid.Framework.Json.Newtonsoft;
using TagKid.Framework.WebApi;
using TagKid.Framework.WebApi.Impl;

namespace TagKid.Core.Bootstrapping.Bootstrappers
{
    public class StartupBootstrapper : IBootstrapper
    {
        public void Bootstrap(IDependencyContainer container)
        {
            container.RegisterSingleton<IJsonSerializer, NewtonsoftJsonSerializer>();
            container.RegisterSingleton<IRouteResolver, RouteResolver>();
            container.RegisterSingleton<IParameterResolver, ParameterResolver>();
            container.RegisterSingleton<IActionInvoker, ActionInvoker>();
            container.RegisterSingleton<IHttpHandler, HttpHandler>();
        }
    }
}