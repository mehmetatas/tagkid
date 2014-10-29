using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Taga.Core.Repository;
using Taga.Core.Repository.Base;

namespace Taga.Repository.EF
{
    public class EFRepository : IRepository
    {
        private readonly DbContext _dbContext;

        public EFRepository()
        {
            var uow = (IEFUnitOfWork)UnitOfWork.Current;
            _dbContext = uow.DbContext;
        }

        private DbSet<T> Set<T>() where T : class
        {
            return _dbContext.Set<T>();
        }

        public void Insert<T>(T entity) where T : class
        {
            SetState(entity, EntityState.Added);
        }

        public void Update<T>(T entity) where T : class
        {
            SetState(entity, EntityState.Modified);
        }

        public void Delete<T>(T entity) where T : class
        {
            SetState(entity, EntityState.Deleted);
        }

        public IQueryable<T> Select<T>() where T : class
        {
            return Set<T>();
        }

        public IList<T> Exec<T>(string spNameOrSql, IDictionary<string, object> args = null, bool rawSql = false) where T : class
        {
            return _dbContext.Database.SqlQuery<T>(spNameOrSql, args == null ? null : args.Values).ToList();
        }

        private void SetState<T>(T entity, EntityState state) where T : class
        {
            var entry = _dbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                Set<T>().Attach(entity);
            }
            entry.State = state;
        }
    }
}
