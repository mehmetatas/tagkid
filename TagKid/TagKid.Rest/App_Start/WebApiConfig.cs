using System.Web.Http;
using Taga.Core.Rest;

namespace TagKid.Rest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cfg = new ControllerConfigurator();

            var serviceConfig = cfg.ControllerFor<IUserService>("users")
                .ActionFor(s => s.GetUser(0), "get", HttpMethodType.Get)
                .ActionFor(s => s.GetUsers(""), "list", HttpMethodType.Get)
                .ActionFor(s => s.SaveUser(null), "save", HttpMethodType.Post)
                .ActionFor(s => s.DeleteUser(0), "delete", HttpMethodType.Delete)
                .Configure();

            config.Routes.MapHttpRoute(
                 "GenericHttpApi",
                 "api/{*.}",
                 new { controller = "GenericHttpApi" },
                 null,
                 new GenericApiHandler(config, serviceConfig)
            );
        }
    }
}
