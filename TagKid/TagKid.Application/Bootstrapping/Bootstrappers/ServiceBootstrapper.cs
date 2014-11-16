using Taga.Core.IoC;
using TagKid.Core.Service;
using TagKid.Service;

namespace TagKid.Application.Bootstrapping.Bootstrappers
{
    public class ServiceBootstrapper : IBootstrapper
    {
        public void Bootstrap(IServiceProvider prov)
        {
            prov.Register<IAuthService, AuthService>();
            prov.Register<IPostService, PostService>();
            prov.Register<ITagService, TagService>();
        }
    }
}