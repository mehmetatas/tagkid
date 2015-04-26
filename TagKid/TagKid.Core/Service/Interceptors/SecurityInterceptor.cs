using TagKid.Core.Models.Messages;
using TagKid.Core.Providers;
using TagKid.Framework.Exceptions;
using TagKid.Framework.WebApi;

namespace TagKid.Core.Service.Interceptors
{
    public class SecurityInterceptor : IActionInterceptor
    {
        private readonly IAuthProvider _authProvider;

        public SecurityInterceptor(IAuthProvider authProvider)
        {
            _authProvider = authProvider;
        }

        public object BeforeCall(RouteContext ctx)
        {
            _authProvider.AuthRoute(ctx);
            return null;
        }

        public void AfterCall(RouteContext ctx)
        {
            
        }

        public object OnException(RouteContext ctx)
        {
            var ex = ctx.Exception as TagKidException;
            return ex == null ? null : Response.Error(ex.Error);
        }

        public void Dispose()
        {
            
        }
    }
}