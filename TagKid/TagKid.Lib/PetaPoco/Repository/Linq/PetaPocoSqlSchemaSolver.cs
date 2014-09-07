using System;
using System.Reflection;
using Taga.Core.Repository.Linq.Sql;

namespace TagKid.Lib.PetaPoco.Repository.Linq
{
    public class PetaPocoSqlSchemaSolver : ILinqSqlSchemaSolver
    {
        public string GetTableName(Type entityType)
        {
            return Mappers.GetMapper(entityType).GetTableInfo(entityType).TableName;
        }

        public string GetColumnName(PropertyInfo propInfo)
        {
            return Mappers.GetMapper(propInfo.ReflectedType).GetColumnInfo(propInfo).ColumnName;
        }
    }
}
