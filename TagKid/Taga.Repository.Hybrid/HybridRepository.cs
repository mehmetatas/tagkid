using System;
using System.Collections.Generic;
using System.Linq;
using Taga.Core.Repository;
using Taga.Core.Repository.Base;
using IQueryProvider = Taga.Core.Repository.IQueryProvider;

namespace Taga.Repository.Hybrid
{
    public class HybridRepository : IRepository
    {
        private readonly IHybridUnitOfWork _uow;
        
        public HybridRepository()
        {
            _uow = (IHybridUnitOfWork) UnitOfWork.Current;
        }

        public void Insert<T>(T entity) where T : class
        {
            _uow.Insert(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _uow.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _uow.Delete(entity);
        }

        public IQueryable<T> Select<T>() where T : class
        {
            return _uow.QueryProvider.Query<T>();
        }

        public IList<T> Exec<T>(string spNameOrSql, IDictionary<string, object> args = null, bool rawSql = false) where T : class
        {
            return _uow.QueryProvider.Exec<T>(spNameOrSql, args, rawSql);
        }
    }
}
