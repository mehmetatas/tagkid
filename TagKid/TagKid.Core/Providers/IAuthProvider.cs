using TagKid.Framework.Hosting;

namespace TagKid.Core.Providers
{
    public interface IAuthProvider
    {
        void AuthRoute(RouteContext ctx);
    }
}
