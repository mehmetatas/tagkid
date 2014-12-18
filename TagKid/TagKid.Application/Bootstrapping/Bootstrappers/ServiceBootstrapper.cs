using Taga.Core.IoC;
using TagKid.Core.Domain;
using TagKid.Core.Service;
using TagKid.Domain;
using TagKid.Service;

namespace TagKid.Application.Bootstrapping.Bootstrappers
{
    public class ServiceBootstrapper : IBootstrapper
    {
        public void Bootstrap(IServiceProvider prov)
        {
            prov.RegisterTransient<IAuthService, AuthService>();
            prov.RegisterTransient<IAuthDomainService, AuthDomainService>();

            prov.RegisterTransient<IPostService, PostService>(); 
            prov.RegisterTransient<IPostDomainService, PostDomainService>();
        }
    }
}