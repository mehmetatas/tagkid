using TagKid.Core.Models.Messages.Auth;
using TagKid.Core.Models.Messages.Post;
using TagKid.Core.Service;
using TagKid.Framework.IoC;
using TagKid.Framework.Owin.Configuration;
using TagKid.Framework.Validation;

namespace TagKid.Core.Bootstrapping.Bootstrappers
{
    class ServiceBootstrapper : IBootstrapper
    {
        public void Bootstrap(IDependencyContainer container)
        {
            var builder = ServiceConfig.Builder();

            BuildPostService(builder);
            BuildAuthService(builder);

            builder.Build();

            ValidationManager.LoadValidatorsFromAssemblyOf<RegisterRequestValidator>();
        }

        private void BuildPostService(ControllerConfigurator builder)
        {
            builder.ControllerFor<IPostService>("post")
                .ActionFor(s => s.Save(default(SaveRequest)), "save", HttpMethod.Post);
        }

        private void BuildAuthService(ControllerConfigurator builder)
        {
            builder.ControllerFor<IAuthService>("auth")
                .ActionFor(s => s.Register(default(RegisterRequest)), "register", HttpMethod.Post).NoAuth()
                .ActionFor(s => s.ActivateRegistration(default(ActivateRegistrationRequest)), "activateRegistration", HttpMethod.Post).NoAuth()
                .ActionFor(s => s.LoginWithPassword(default(LoginWithPasswordRequest)), "loginWithPassword", HttpMethod.Post).NoAuth();
        }
    }
}
