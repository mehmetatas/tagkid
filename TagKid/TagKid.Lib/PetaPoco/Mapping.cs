using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace TagKid.Lib.PetaPoco
{
    class Mapping
    {
        private static readonly CultureInfo EnGb = new CultureInfo("en-GB");

        private Mapping(Type type, string tableName)
        {
            EntityType = type;
            TableInfo = new TableInfo
            {
                TableName = tableName
            };

            ColumnMappings = new Dictionary<PropertyInfo, ColumnInfo>();
        }

        public Type EntityType { get; private set; }
        public TableInfo TableInfo { get; private set; }
        public Dictionary<PropertyInfo, ColumnInfo> ColumnMappings { get; private set; }

        public static Builder<T> Map<T>(string tableName = null) where T : class,new()
        {
            return new Builder<T>(tableName);
        }

        public class Builder<T> where T : class,new()
        {
            private readonly Mapping _mapping;

            public Builder(string tableName = null)
            {
                var type = typeof(T);
                if (String.IsNullOrWhiteSpace(tableName))
                {
                    tableName = ToDbName(type.Name);

                    if (tableName.EndsWith("y"))
                        tableName = tableName.Substring(0, tableName.Length - 1) + "ies";
                    else
                        tableName += "s";
                }

                _mapping = new Mapping(type, tableName);

                foreach (var propInf in type.GetProperties())
                {
                    _mapping.ColumnMappings.Add(propInf, new ColumnInfo { ColumnName = ToDbName(propInf.Name) });
                }
            }

            public Builder<T> WithPrimaryKey(string primaryKeyColumnName, bool autoIncrement = true)
            {
                _mapping.TableInfo.PrimaryKey = primaryKeyColumnName;
                _mapping.TableInfo.AutoIncrement = autoIncrement;
                return this;
            }

            public Builder<T> Map(Expression<Func<T, dynamic>> propExpression, string columnName)
            {
                if (propExpression == null)
                    throw new ArgumentNullException("propExpression");

                if (String.IsNullOrWhiteSpace(columnName))
                    throw new ArgumentException("Column name cannot be empty!", "columnName");

                var propInf = ((MemberExpression)propExpression.Body).Member as PropertyInfo;

                if (propInf == null)
                    throw new InvalidOperationException("Expression must be a MemberExpression to a property!");

                _mapping.ColumnMappings[propInf].ColumnName = columnName;

                return this;
            }

            private static string ToDbName(string clrName)
            {
                var sb = new StringBuilder();

                for (var i = 0; i < clrName.Length; i++)
                {
                    var c = clrName[i];
                    if (Char.IsLower(c))
                    {
                        sb.Append(c);
                    }
                    else
                    {
                        if (i != 0)
                            sb.Append("_");
                        sb.Append(Char.ToLower(c, EnGb));
                    }
                }

                return sb.ToString();
            }

            public Mapping Build()
            {
                return _mapping;
            }
        }
    }
}