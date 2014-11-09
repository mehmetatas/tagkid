using Taga.Core.IoC;
using Taga.IoC.Ninject;
using TagKid.Application.Bootstrapping.Bootstrappers;

namespace TagKid.Application.Bootstrapping
{
    public static class Bootstrapper
    {
        public static void StartApp()
        {
            ServiceProvider.Init(new NinjectServiceProvider());

            Bootstrap(
                new StartupBootstrapper(),
                new DatabaseBootstrapper(),
                new RepositoryBootstrapper(),
                new ServiceBootstrapper(),
                new MappingBootstrapper());
        }

        public static void Bootstrap(params IBootstrapper[] bootstrappers)
        {
            foreach (var bootstrapper in bootstrappers)
            {
                bootstrapper.Bootstrap(ServiceProvider.Provider);
            }
        }
    }
}