using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using IQueryProvider = Taga.Core.Repository.IQueryProvider;

namespace Taga.Repository.EF
{
    public abstract class EFQueryProvider : IQueryProvider
    {
        private DbContext _dbContext;

        public void SetConnection(IDbConnection connection)
        {
            _dbContext = CreateDbContext((DbConnection) connection);
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return _dbContext.Set<T>();
        }

        public IList<T> Exec<T>(string spNameOrSql, IDictionary<string, object> args = null, bool rawSql = false) where T : class
        {
            return _dbContext.Database.SqlQuery<T>(spNameOrSql, args == null ? null : args.Values).ToList();
        }

        protected abstract DbContext CreateDbContext(DbConnection connection);
    }
}
