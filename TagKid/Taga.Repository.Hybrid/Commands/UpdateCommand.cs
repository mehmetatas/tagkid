using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Text;

namespace Taga.Repository.Hybrid.Commands
{
    class UpdateCommand : BaseCommand
    {
        private static readonly Hashtable QueryCache = new Hashtable();

        public UpdateCommand(object entity)
            : base(entity)
        {
        }

        public override void Execute(IDbConnection conn)
        {
            var cmd = conn.CreateCommand();

            if (QueryCache.ContainsKey(EntityType))
            {
                cmd.CommandText = (string)QueryCache[EntityType];
            }
            else
            {
                cmd.CommandText = BuildSqlQuery();
            }

            var tableMapping = MappingProvider.GetTableMapping(EntityType);

            foreach (var columnMapping in tableMapping.Columns)
            {
                var param = cmd.CreateParameter();

                param.Value = columnMapping.PropertyInfo.GetValue(Entity);
                param.ParameterName = GetParamName(columnMapping.ColumnName);

                cmd.Parameters.Add(param);
            }

            cmd.ExecuteNonQuery();
        }

        private string BuildSqlQuery()
        {
            var tableMapping = MappingProvider.GetTableMapping(Entity.GetType());

            var setClause = tableMapping.UpdateColumns
                .Select(col => String.Format("{0}={1}", col.ColumnName, GetParamName(col.ColumnName)))
                .ToArray();

            var whereClause = tableMapping.IdColumns
                .Select(col => String.Format("{0}={1}", col.ColumnName, GetParamName(col.ColumnName)))
                .ToArray();

            var sb = new StringBuilder("UPDATE ")
                .Append(tableMapping.TableName)
                .Append(" SET ")
                .Append(String.Join(",", setClause))
                .Append(" WHERE ")
                .Append(String.Join(" AND ", whereClause));

            var sql = sb.ToString();

            lock (QueryCache)
            {
                if (QueryCache.ContainsKey(EntityType))
                {
                    return (string)QueryCache[EntityType];
                }
                QueryCache.Add(EntityType, sql);
            }

            return sql;
        }
    }
}