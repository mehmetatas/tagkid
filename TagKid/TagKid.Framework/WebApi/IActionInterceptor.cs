using System;

namespace TagKid.Framework.WebApi
{
    public interface IActionInterceptor : IDisposable
    {
        object BeforeCall(RouteContext ctx);

        void AfterCall(RouteContext ctx);

        object OnException(RouteContext ctx);
    }
}