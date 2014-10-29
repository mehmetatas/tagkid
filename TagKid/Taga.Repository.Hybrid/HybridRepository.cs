using System;
using System.Collections.Generic;
using System.Linq;
using Taga.Core.Repository;
using Taga.Core.Repository.Base;

namespace Taga.Repository.Hybrid
{
    public class HybridRepository : IRepository
    {
        private readonly IQueryProvider _queryProvider;
        private readonly IHybridUnitOfWork _uow;

        public HybridRepository(IQueryProvider queryProvider)
        {
            _queryProvider = queryProvider;
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
            return _queryProvider.Query<T>();
        }

        public IList<T> Exec<T>(string spNameOrSql, IDictionary<string, object> args = null, bool rawSql = false) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
