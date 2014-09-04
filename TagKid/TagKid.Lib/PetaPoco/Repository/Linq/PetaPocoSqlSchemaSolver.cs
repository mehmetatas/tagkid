using System;
using System.Reflection;
using Taga.Core.Repository.Linq.Sql;

namespace TagKid.Lib.PetaPoco.Repository.Linq
{
    public class PetaPocoSqlSchemaSolver : ILinqSqlSchemaSolver
    {
        public string GetTableName(Type entityType)
        {
            return TableInfo.FromPoco(entityType).TableName;
        }

        public string GetColumnName(PropertyInfo propInfo)
        {
            return ColumnInfo.FromProperty(propInfo).ColumnName;
        }
    }
}
