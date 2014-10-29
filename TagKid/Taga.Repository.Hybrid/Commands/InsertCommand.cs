using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Text;

namespace Taga.Repository.Hybrid.Commands
{
    class InsertCommand : BaseCommand
    {
        private static readonly Hashtable QueryCache = new Hashtable();

        public InsertCommand(object entity)
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

            foreach (var columnMapping in tableMapping.InsertColumns)
            {
                var param = cmd.CreateParameter();

                param.Value = columnMapping.PropertyInfo.GetValue(Entity);
                param.ParameterName = GetParamName(columnMapping.ColumnName);

                cmd.Parameters.Add(param);
            }

            var id = cmd.ExecuteScalar();

            if (tableMapping.HasSingleAutoIncrementId)
            {
                var prop = tableMapping.IdColumns[0].PropertyInfo;
                prop.SetValue(Entity, Convert.ChangeType(id, prop.PropertyType));
            }
        }

        private string BuildSqlQuery()
        {
            var tableMapping = MappingProvider.GetTableMapping(Entity.GetType());

            var columnsToInsert = tableMapping.InsertColumns
                .Select(col => col.ColumnName)
                .ToArray();

            var paramNames = columnsToInsert
                .Select(GetParamName)
                .ToArray();

            var sb = new StringBuilder("INSERT INTO ")
                .Append(tableMapping.TableName)
                .Append(" (")
                .Append(String.Join(",", columnsToInsert))
                .Append(") VALUES (")
                .Append(String.Join(",", paramNames))
                .Append(")");

            var sql = sb.ToString();

            if (tableMapping.HasSingleAutoIncrementId)
            {
                sql = HybridDbProvider.AppendSelectIdentity(sql);
            }

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