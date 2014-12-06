﻿using System.Web.Mvc;
using System.Web.Routing;

namespace TagKid.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ViewAction",
                url: "{controller}/{action}",
                defaults: new { controller = "UI", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{*.}",
                defaults: new { controller = "UI", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}