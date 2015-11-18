using TagKid.Core.Domain;
using TagKid.Core.Domain.Impl;
using TagKid.Core.Mail;
using TagKid.Core.Providers;
using TagKid.Core.Providers.Impl;
using TagKid.Core.Repository;
using TagKid.Core.Repository.Impl;
using TagKid.Core.Service;
using TagKid.Core.Service.Impl;
using TagKid.Core.Service.Interceptors;
using TagKid.Framework.Hosting;
using TagKid.Framework.Hosting.Impl;
using TagKid.Framework.IoC;
using TagKid.Framework.Json;
using TagKid.Framework.Json.Newtonsoft;
using TagKid.Framework.UnitOfWork;
using TagKid.Framework.UnitOfWork.Impl;

namespace TagKid.Core.Bootstrapping.Bootstrappers
{
    class DependencyBootstrapper : IBootstrapper
    {
        public void Bootstrap(IDependencyContainer container)
        {
            // Framework "er"s
            container.RegisterSingleton<IJsonSerializer, NewtonsoftJsonSerializer>();
            container.RegisterSingleton<IRouteResolver, RouteResolver>();
            container.RegisterSingleton<IParameterResolver, ParameterResolver>();
            container.RegisterSingleton<IActionInvoker, ActionInvoker>();
            container.RegisterSingleton<IHttpRequestHandler, HttpRequestHandler>();

            // Database
            container.RegisterTransient<IUnitOfWork, UnitOfWork>();
            container.RegisterSingleton<IRepository, Framework.UnitOfWork.Impl.Repository>();
            container.RegisterSingleton<IAdoRepository, AdoRepository>();

            // Interceptors
            container.RegisterSingleton<IActionInterceptorBuilder, TagKidActionInterceptorBuilder>();

            // Providers
            container.RegisterSingleton<IAuthProvider, AuthProvider>();
            container.RegisterSingleton<ICryptoProvider, CryptoProvider>();
            container.RegisterSingleton<IMailProvider, MailProvider>();

            // Repositories
            container.RegisterSingleton<IPostRepository, PostRepository>();
            container.RegisterSingleton<IUserRepository, UserRepository>();
            container.RegisterSingleton<IConfirmationCodeRepository, ConfirmationCodeRepository>();

            // Auth
            container.RegisterSingleton<IAuthService, AuthService>();
            container.RegisterSingleton<IAuthDomain, AuthDomain>();

            // Post
            container.RegisterSingleton<IPostService, PostService>();
            container.RegisterSingleton<IPostDomain, PostDomain>();
        }
    }
}