using Taga.Core.IoC;
using TagKid.Lib.Services;
using TagKid.Lib.Services.Impl;

namespace TagKid.Lib.Bootstrapping.Bootstrappers
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
