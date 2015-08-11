using TagKid.Core.Bootstrapping.Bootstrappers;
using TagKid.Framework.IoC;
using TagKid.Framework.IoC.Castle;
using TagKid.Framework.WebApi;

namespace TagKid.Core.Bootstrapping
{
    public static class Bootstrapper
    {
        public static void StartApp()
        {
            DependencyContainer.Init(new CastleDependencyContainer());

            Bootstrap(
                new DependencyBootstrapper(),
                new DatabaseBootstrapper(),
                new ServiceBootstrapper());

            WebApi.Init();
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