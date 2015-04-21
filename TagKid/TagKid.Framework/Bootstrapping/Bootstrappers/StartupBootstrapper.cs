using TagKid.Framework.IoC;
using TagKid.Framework.Json;
using TagKid.Framework.Json.Impl;
using TagKid.Framework.Repository.Mapping;

namespace TagKid.Framework.Bootstrapping.Bootstrappers
{
    public class StartupBootstrapper : IBootstrapper
    {
        public void Bootstrap(IDependencyContainer container)
        {
            container.RegisterSingleton<IMappingProvider, MappingProvider>();
            container.RegisterSingleton<IJsonSerializer, NewtonsoftJsonSerializer>();
        }
    }
}