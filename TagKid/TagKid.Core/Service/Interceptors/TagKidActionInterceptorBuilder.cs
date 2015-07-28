using DummyOrm.Db;
using TagKid.Core.Providers;
using TagKid.Framework.WebApi;

namespace TagKid.Core.Service.Interceptors
{
    public class TagKidActionInterceptorBuilder : IActionInterceptorBuilder
    {
        private readonly IDbFactory _dbFactory;
        private readonly IAuthProvider _authProvider;

        public TagKidActionInterceptorBuilder(IDbFactory uow, IAuthProvider authProvider)
        {
            _dbFactory = uow;
            _authProvider = authProvider;
        }

        public IActionInterceptor Build(RouteContext context)
        {
            return new TagKidActionInterceptor(
                new UnitOfWorkInterceptor(_dbFactory),
                new SecurityInterceptor(_authProvider));
        }
    }
}
