using Taga.Core.IoC;
using Taga.Core.Mapping;

namespace TagKid.Application.Bootstrapping.Bootstrappers
{
    public class MappingBootstrapper : IBootstrapper
    {
        public void Bootstrap(IServiceProvider prov)
        {
            var mapper = prov.GetOrCreate<IMapper>();
        }
    }
}