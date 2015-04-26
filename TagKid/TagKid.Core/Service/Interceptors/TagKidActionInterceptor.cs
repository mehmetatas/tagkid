using TagKid.Framework.WebApi;

namespace TagKid.Core.Service.Interceptors
{
    public class TagKidActionInterceptor : IActionInterceptor
    {
        private readonly IActionInterceptor[] _interceptors;

        public TagKidActionInterceptor(params IActionInterceptor[] interceptors)
        {
            _interceptors = interceptors;
        }

        public object BeforeCall(RouteContext ctx)
        {
            foreach (var interceptor in _interceptors)
            {
                var res = interceptor.BeforeCall(ctx);
                if (res != null)
                {
                    return res;
                }
            }
            return null;
        }

        public void AfterCall(RouteContext ctx)
        {
            foreach (var interceptor in _interceptors)
            {
                interceptor.AfterCall(ctx);
            }
        }

        public object OnException(RouteContext ctx)
        {
            foreach (var interceptor in _interceptors)
            {
                var res = interceptor.OnException(ctx);
                if (res != null)
                {
                    return res;
                }
            }
            return null;
        }

        public void Dispose()
        {
            foreach (var interceptor in _interceptors)
            {
                interceptor.Dispose();
            }
        }
    }
}
