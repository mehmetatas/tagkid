using System.Collections.Generic;
using System.Linq;

namespace Taga.Repository.Hybrid
{
    public interface IQueryProvider
    {
        IQueryable<T> Query<T>() where T : class;

        IList<T> Exec<T>(string spNameOrSql, IDictionary<string, object> args = null, bool rawSql = false)
            where T : class;
    }
}
