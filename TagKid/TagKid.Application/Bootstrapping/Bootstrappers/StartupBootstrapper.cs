using Taga.Core.IoC;
using Taga.Core.Json;
using Taga.Core.Mapping;
using Taga.Core.Repository.Mapping;
using Taga.Core.Rest;
using Taga.Json.Newtonsoft;
using Taga.Mapping.AutoMapper;
using TagKid.Core.Database;
using TagKid.Core.Service;

namespace TagKid.Application.Bootstrapping.Bootstrappers
{
    public class StartupBootstrapper : IBootstrapper
    {
        public void Bootstrap(IServiceProvider prov)
        {
            prov.RegisterSingleton<IMappingProvider>(new MappingProvider());
            prov.RegisterSingleton<IActionInterceptor>(new TagKidActionInterceptor());
            prov.RegisterSingleton<IJsonSerializer>(new NewtonsoftJsonSerializer());
            prov.RegisterSingleton<IPropertyFilter>(new TagKidPropertyFilter());
            prov.RegisterSingleton<IMapper>(new AutoMapper());
            prov.Register<IApiCallHandler, DefaultApiCallHandler>();
        }
    }
}