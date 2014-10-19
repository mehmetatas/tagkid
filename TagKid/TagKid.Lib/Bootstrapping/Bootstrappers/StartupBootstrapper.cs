using Taga.Core.IoC;
using Taga.Core.Mapping;

namespace TagKid.Lib.Bootstrapping.Bootstrappers
{
    public class StartupBootstrapper : IBootstrapper
    {
        public void Bootstrap(IServiceProvider prov)
        {
            prov.Register<IMapper, Mapper>();
        }
    }
}