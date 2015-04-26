using TagKid.Core.Providers;
using TagKid.Framework.Repository;
using TagKid.Framework.WebApi;

namespace TagKid.Core.Service.Interceptors
{
    public class TagKidActionInterceptorBuilder : IActionInterceptorBuilder
    {
        private readonly IUnitOfWork _uow;
        private readonly IAuthProvider _authProvider;

        public TagKidActionInterceptorBuilder(IUnitOfWork uow, IAuthProvider authProvider)
        {
            _uow = uow;
            _authProvider = authProvider;
        }

        public IActionInterceptor Build(RouteContext context)
        {
            return new TagKidActionInterceptor(
                new UnitOfWorkInterceptor(_uow),
                new SecurityInterceptor(_authProvider));
        }
    }
}
