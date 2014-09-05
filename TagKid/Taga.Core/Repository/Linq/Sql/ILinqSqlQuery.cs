using Taga.Core.Repository.Sql;

namespace Taga.Core.Repository.Linq.Sql
{
    public interface ILinqSqlQuery : ILinqQuery, ISql
    {
        int PageIndex { get; }
        int PageSize { get; }
    }
}
