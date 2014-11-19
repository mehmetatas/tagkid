using Taga.Core.IoC;
using Taga.Core.Rest;
using TagKid.Core.Models.DTO.Messages.Auth;
using TagKid.Core.Service;
using TagKid.Core.Service.Interceptors;

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

            prov.RegisterTransient<IActionInterceptor, ActionInterceptor>();
            prov.RegisterSingleton<IApiCallHandler>(new DefaultApiCallHandler());
        }

        private void BuildAuthService(ControllerConfigurator cfg)
        {
            cfg.ControllerFor<IAuthService>("auth")
                .ActionFor(s => s.SignUpWithEmail(default(SignUpWithEmailRequest)), "signup");
        }

        private void BuildUserService(ControllerConfigurator cfg)
        {

        }

        private void BuildPostService(ControllerConfigurator cfg)
        {

        }
    }
}