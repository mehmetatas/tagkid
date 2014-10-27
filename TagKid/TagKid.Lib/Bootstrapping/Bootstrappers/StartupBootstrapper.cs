using Taga.Core.IoC;
using Taga.Core.Json;
using Taga.Core.Mapping;
using Taga.Json.Newtonsoft;
using Taga.Mapping.AutoMapper;

namespace TagKid.Lib.Bootstrapping.Bootstrappers
{
    public class StartupBootstrapper : IBootstrapper
    {
        public void Bootstrap(IServiceProvider prov)
        {
            prov.Register<IMapper, AutoMapper>();
            prov.Register<IJsonSerializer, NewtonsoftJsonSerializer>();
        }
    }
}