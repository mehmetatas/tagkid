using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using TagKid.Framework.IoC;
using TagKid.Framework.Models.Database;
using TagKid.Framework.Repository.Mapping;

namespace TagKid.Framework.Repository
{
    public interface IRepository
    {
        void Insert<T>(T entity) where T : class, new();

        void Update<T>(T entity) where T : class, new();

        void Delete<T>(T entity) where T : class, new();

        IQueryable<T> Select<T>() where T : class, new();

        T GetById<T>(Guid id) where T : class, new();
    }

    public static class RepositoryExtensions
    {
        public static void Save<T>(this IRepository repo, T entity) where T : class, IEntity, new()
        {
            var isNew = entity.Id == 0L;

            if (isNew)
            {
                repo.Insert(entity);
            }
            else
            {
                repo.Update(entity);
            }
        }

        public static void Delete<T>(this IAdoRepository repo, Expression<Func<T, object>> propExpression,
            params object[] values) where T : class, new()
        {
            MemberExpression memberExp;

            var body = propExpression.Body;
            if (body is UnaryExpression)
            {
                memberExp = (MemberExpression)((UnaryExpression)body).Operand;
            }
            else
            {
                memberExp = (MemberExpression)body;
            }

            var propInf = (PropertyInfo)memberExp.Member;

            var mappingProv = DependencyContainer.Current.Resolve<IMappingProvider>();

            var tableMapping = mappingProv.GetTableMapping<T>();

            var columnMapping = tableMapping.Columns.First(cm => cm.PropertyInfo == propInf);

            var paramNames = Enumerable.Range(0, values.Length).Select(i => String.Format("p_{0}", i)).ToArray();

            var sql = new StringBuilder("DELETE FROM ")
                .Append(tableMapping.TableName)
                .Append(" WHERE ")
                .Append(columnMapping.ColumnName)
                .Append(" IN (")
                .Append(String.Join(",", paramNames.Select(p => String.Format("~{0}", p))))
                .Append(")")
                .ToString();

            var args = Enumerable.Range(0, values.Length).ToDictionary(i => paramNames[i], i => values[i]);

            repo.ExecuteNonQuery(sql, args);
        }

    }
}
