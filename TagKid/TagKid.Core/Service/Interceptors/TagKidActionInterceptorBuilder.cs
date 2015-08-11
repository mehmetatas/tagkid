using TagKid.Core.Providers;
using TagKid.Framework.WebApi;

namespace TagKid.Core.Service.Interceptors
{
    public class TagKidActionInterceptorBuilder : IActionInterceptorBuilder
    {
        private readonly IAuthProvider _authProvider;

        public TagKidActionInterceptorBuilder(IAuthProvider authProvider)
        {
            _authProvider = authProvider;
        }

        public IActionInterceptor Build(RouteContext context)
        {
            return new TagKidActionInterceptor(
                new UnitOfWorkInterceptor(),
                new SecurityInterceptor(_authProvider));
        }
    }
}
