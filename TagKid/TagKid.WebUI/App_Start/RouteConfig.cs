using System.Web.Mvc;
using System.Web.Routing;

namespace TagKid.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Layout",
                url: "Layout/{action}",
                defaults: new { controller = "Layout" }
            );

            routes.MapRoute(
                name: "Pages",
                url: "Pages/{action}",
                defaults: new { controller = "Pages" }
            );

            routes.MapRoute(
                name: "Directives",
                url: "Directives/{action}",
                defaults: new { controller = "Directives" }
            );

            routes.MapRoute(
                name: "Dialogs",
                url: "Dialogs/{action}",
                defaults: new { controller = "Dialogs" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{*.}",
                defaults: new { controller = "Layout", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}