using TagKid.Core.Models.Messages.Post;
using TagKid.Core.Service;
using TagKid.Framework.IoC;
using TagKid.Framework.WebApi.Configuration;

namespace TagKid.Core.Bootstrapping.Bootstrappers
{
    class ServiceBootstrapper : IBootstrapper
    {
        public void Bootstrap(IDependencyContainer container)
        {
            var builder = ServiceConfig.Builder();

            BuildPostService(builder);

            builder.Build();
        }

        private void BuildPostService(ControllerConfigurator builder)
        {
            builder.ControllerFor<IPostService>("post")
                .ActionFor(p => p.Save(default(SaveRequest)), "save", HttpMethod.Post)
                .ActionFor(p => p.Dummy(default(DummyRequest)), "dummy", HttpMethod.Get).NoAuth();
        }
    }
}
