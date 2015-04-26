using TagKid.Framework.Repository;
using TagKid.Framework.WebApi;

namespace TagKid.Core.Service.Interceptors
{
    public class UnitOfWorkInterceptor : IActionInterceptor
    {
        private readonly IUnitOfWork _uow;

        public UnitOfWorkInterceptor(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public object BeforeCall(RouteContext ctx)
        {
            _uow.BeginTransaction();
            return null;
        }

        public void AfterCall(RouteContext ctx)
        {
            _uow.Commit();
        }

        public object OnException(RouteContext ctx)
        {
            _uow.Rollback();
            return null;
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
