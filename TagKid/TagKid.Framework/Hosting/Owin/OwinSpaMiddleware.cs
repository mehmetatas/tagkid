using System.Threading.Tasks;
using Microsoft.Owin;

namespace TagKid.Framework.Hosting.Owin
{
    public class OwinSpaMiddleware : OwinMiddleware
    {
        public OwinSpaMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            await Next.Invoke(context);

            if (context.Response.StatusCode == 404)
            {
                context.Response.Redirect("/");
            }
        }
    }
}