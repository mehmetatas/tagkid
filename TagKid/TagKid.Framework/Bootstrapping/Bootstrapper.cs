using TagKid.Framework.Bootstrapping.Bootstrappers;
using TagKid.Framework.IoC;
using TagKid.Framework.IoC.Impl;

namespace TagKid.Framework.Bootstrapping
{
    public static class Bootstrapper
    {
        public static void StartApp()
        {
            DependencyContainer.Init(new CastleDependencyContainer());

            Bootstrap(
                new StartupBootstrapper(),
                new DatabaseBootstrapper());
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