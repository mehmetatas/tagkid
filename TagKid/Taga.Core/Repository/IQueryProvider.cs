using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Taga.Core.Repository
{
    public interface IQueryProvider
    {
        void SetConnection(IDbConnection connection);

        IQueryable<T> Query<T>() where T : class;

        IList<T> Exec<T>(string spNameOrSql, IDictionary<string, object> args = null, bool rawSql = false)
            where T : class;
    }
}
