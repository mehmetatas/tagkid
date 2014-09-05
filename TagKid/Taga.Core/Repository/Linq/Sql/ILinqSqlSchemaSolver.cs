using System;
using System.Reflection;

namespace Taga.Core.Repository.Linq.Sql
{
    public interface ILinqSqlSchemaSolver
    {
        string GetTableName(Type entityType);

        string GetColumnName(PropertyInfo propertyInfo);
    }
}
