using TagKid.Core.Bootstrapping.Bootstrappers;
using TagKid.Framework.IoC;
using TagKid.Framework.IoC.Castle;

namespace TagKid.Core.Bootstrapping
{
    public static class Bootstrapper
    {
        public static void StartApp()
        {
            DependencyContainer.Init(new CastleDependencyContainer());

            Bootstrap(
                new StartupBootstrapper(),
                new DatabaseBootstrapper(),
                new ServiceBootstrapper());
        }

        public static void Bootstrap(params IBootstrapper[] bootstrappers)
        {
            foreach (var bootstrapper in bootstrappers)
            {
                bootstrapper.Bootstrap(DependencyContainer.Current);
            }
        }
    }
}