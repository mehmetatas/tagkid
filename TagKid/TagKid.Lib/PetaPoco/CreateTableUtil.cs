using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TagKid.Lib.PetaPoco
{
    public static class CreateTableUtil
    {

        /*
CREATE TABLE `users` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `fullname` varchar(40) NOT NULL,
  `email` varchar(80) NOT NULL,
  `username` varchar(20) NOT NULL,
  `password` char(64) NOT NULL,
  `join_date` datetime NOT NULL,
  `profile_image_url` varchar(256) NOT NULL,
  `facebook_id` varchar(20) NOT NULL,
  `type` int(11) NOT NULL,
  `status` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `username` (`username`),
  KEY `email` (`email`),
  KEY `status` (`status`) USING BTREE
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

         */
        public static string GenerateCreateTableSql(string asmFile, string ns)
        {
            var asm = Assembly.LoadFrom(asmFile);
            var types = asm.GetTypes().Where(type => type.Namespace == ns && type.IsClass);

            var sql = new StringBuilder();

            foreach (var type in types)
            {
                var tableNameAttr = type.GetCustomAttribute<TableNameAttribute>();

                if (tableNameAttr == null)
                    continue;

                sql.AppendFormat("drop table if exists `{0}`;", tableNameAttr.Value)
                    .AppendLine()
                    .AppendFormat("create table `{0}` (", tableNameAttr.Value)
                    .AppendLine();

                var primaryKeyAttr = type.GetCustomAttribute<PrimaryKeyAttribute>();
                var props = type.GetProperties();

                foreach (var prop in props)
                {
                    var colAttr = prop.GetCustomAttribute<ColumnAttribute>();

                    if (colAttr == null)
                        continue;

                    sql.AppendFormat("    `{0}` {1} not null", colAttr.Name, GetDbTypeName(prop.PropertyType));

                    if (primaryKeyAttr != null && colAttr.Name == primaryKeyAttr.Value && primaryKeyAttr.autoIncrement)
                        sql.Append(" auto_increment");

                    sql.Append(",")
                        .AppendLine();
                }

                if (primaryKeyAttr != null)
                {
                    sql.AppendFormat("    PRIMARY KEY (`{0}`),", primaryKeyAttr.Value)
                        .AppendLine();
                }

                sql.AppendLine("    -- KEY `KEY_` (``) -- USING BTREE");

                sql.Append(") ENGINE=MyISAM DEFAULT CHARSET=utf8;")
                        .AppendLine()
                        .AppendLine()
                        .AppendLine();
            }

            return sql.ToString();
        }

        private static string GetDbTypeName(Type propType)
        {
            if (propType == typeof (string))
                return "varchar()";
            if (propType == typeof(int) || propType.IsEnum)
                return "int(11)";
            if (propType == typeof(long))
                return "bigint(20)";
            if (propType == typeof(DateTime))
                return "datetime";

            throw new Exception("Unknown type: " + propType);

        }
    }
}
