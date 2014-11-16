using Taga.Core.IoC;
using Taga.Core.Rest;
using TagKid.Core.Models.DTO.Messages;
using TagKid.Core.Service;

namespace TagKid.Application.Bootstrapping.Bootstrappers
{
    public class RestBootstrapper : IBootstrapper
    {
        public void Bootstrap(IServiceProvider prov)
        {
            var cfg = ServiceConfig.Builder();

            BuildAuthService(cfg);
            BuildUserService(cfg);
            BuildPostService(cfg);

            cfg.Build();
        }

        private void BuildAuthService(ControllerConfigurator cfg)
        {
            cfg.ControllerFor<IAuthService>("auth")
                .ActionFor(s => s.SignUp(default(SignUpRequest)), "signup");
        }

        private void BuildUserService(ControllerConfigurator cfg)
        {

        }

        private void BuildPostService(ControllerConfigurator cfg)
        {

        }
    }
}