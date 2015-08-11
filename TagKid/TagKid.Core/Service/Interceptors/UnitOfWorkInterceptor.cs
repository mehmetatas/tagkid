﻿using TagKid.Framework.IoC;
using TagKid.Framework.UnitOfWork;
using TagKid.Framework.WebApi;
using TagKid.Framework.WebApi.Configuration;

namespace TagKid.Core.Service.Interceptors
{
    public class UnitOfWorkInterceptor : IActionInterceptor
    {
        private IUnitOfWork _uow;

        public object BeforeCall(RouteContext ctx)
        {
            _uow = DependencyContainer.Current.Resolve<IUnitOfWork>();

            if (ctx.Method.HttpMethod != HttpMethod.Get)
            {
                _uow.BeginTransaction();
            }
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
