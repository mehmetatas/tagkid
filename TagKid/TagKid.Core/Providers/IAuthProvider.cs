using TagKid.Framework.WebApi;

namespace TagKid.Core.Providers
{
    public interface IAuthProvider
    {
        void AuthRoute(RouteContext ctx);
    }
}
