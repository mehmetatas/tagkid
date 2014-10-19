using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using FluentNHibernate.Mapping;

namespace TagKid.Lib.NHibernate.Mappings
{
    public class NHAutoMap<T> : ClassMap<T>
    {
        private static readonly CultureInfo EnGb = new CultureInfo("en-GB");

        public NHAutoMap(string tableName = null, string idProperty = "Id")
        {
            var type = typeof (T);

            if (tableName == null)
                tableName = GetTableName();

            Table(tableName);

            foreach (var propInf in type.GetProperties())
            {
                var parameter = Expression.Parameter(type, "entity");
                var property = Expression.Property(parameter, propInf);
                var body = Expression.Convert(property, typeof (object));
                var funcType = typeof (Func<T, object>);
                var lambda = (Expression<Func<T, object>>) Expression.Lambda(funcType, body, parameter);

                if (propInf.Name == idProperty)
                {
                    var idPart = Id(lambda, ToDbName(idProperty));

                    if (idProperty != "Id")
                    {
                        idPart.GeneratedBy.Assigned();
                    }
                }

                var propPart = Map(lambda, ToDbName(propInf.Name));

                if (propInf.PropertyType.IsEnum)
                    propPart.CustomType(propInf.PropertyType);
            }
        }

        private static string GetTableName()
        {
            var type = typeof (T);

            var tableName = ToDbName(type.Name);

            if (tableName.EndsWith("y"))
                tableName = tableName.Substring(0, tableName.Length - 1) + "ies";
            else
                tableName += "s";

            return tableName;
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
    }
}