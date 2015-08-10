using DummyOrm.Db;
using TagKid.Framework.WebApi;
using TagKid.Framework.WebApi.Configuration;

namespace TagKid.Core.Service.Interceptors
{
    public class UnitOfWorkInterceptor : IActionInterceptor
    {
        private IDb _db;
        private readonly IDbFactory _factory;

        public UnitOfWorkInterceptor(IDbFactory uow)
        {
            _factory = uow;
        }

        public object BeforeCall(RouteContext ctx)
        {
            _db = _factory.Create();
            if (ctx.Method.HttpMethod != HttpMethod.Get)
            {
                _db.BeginTransaction();
            }
            return null;
        }

        public void AfterCall(RouteContext ctx)
        {
            _db.Commit();
        }

        public object OnException(RouteContext ctx)
        {
            _db.Rollback();
            return null;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
