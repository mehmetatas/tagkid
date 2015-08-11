using System;
using System.Web;
using TagKid.Core.Bootstrapping;

namespace TagKid.WebUI
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Bootstrapper.StartApp();
        }
    }
}