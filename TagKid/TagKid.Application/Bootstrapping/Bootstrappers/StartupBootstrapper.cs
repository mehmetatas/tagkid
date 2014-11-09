using Taga.Core.IoC;
using Taga.Core.Json;
using Taga.Core.Mapping;
using Taga.Core.Repository.Mapping;
using Taga.Json.Newtonsoft;
using Taga.Mapping.AutoMapper;
using TagKid.Core.Database;

namespace TagKid.Application.Bootstrapping.Bootstrappers
{
    public class StartupBootstrapper : IBootstrapper
    {
        public void Bootstrap(IServiceProvider prov)
        {
            prov.RegisterSingleton<IMappingProvider>(new MappingProvider());
            prov.Register<IMapper, AutoMapper>();
            prov.Register<IJsonSerializer, NewtonsoftJsonSerializer>();
            prov.RegisterSingleton<IPropertyFilter>(new TagKidPropertyFilter());
        }
    }
}