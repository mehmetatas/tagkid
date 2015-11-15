using System;
using System.Web.Http;

namespace TagKid.Framework.Hosting.WebApi
{
    [Obsolete("Use TagKid.Framework.Hosting.Owin")]
    public static class WebApi
    {
        public static void Init()
        {
            var cfg = GlobalConfiguration.Configuration;

            cfg.Routes.MapHttpRoute(
                "GenericHttpApi",
                "api/{*.}",
                new { controller = "GenericHttpApi" },
                null,
                new GenericWebApiHandler(cfg));
        }
    }
}