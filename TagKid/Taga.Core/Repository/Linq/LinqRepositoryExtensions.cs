using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Taga.Core.Repository.Linq.Sql;

namespace Taga.Core.Repository.Linq
{
    public static class LinqRepositoryExtensions
    {
        internal static bool EndsWith(this StringBuilder sb, string test)
        {
            if (sb.Length < test.Length)
                return false;

            string end = sb.ToString(sb.Length - test.Length, test.Length);
            return end.Equals(test);
        }

        internal static StringBuilder RemoveLast(this StringBuilder sb, int length)
        {
            return sb.Remove(sb.Length - length, length);
        }

        public static IPage<TEntity> Query<TEntity>(this ILinqRepository repo, Expression<Func<TEntity, bool>> whereExpression, int pageIndex, int pageSize)
        {
            var sqlQuery = new LinqSqlQueryBuilder<TEntity>();

            if (whereExpression != null)
                sqlQuery.Where(whereExpression);

            if (pageIndex > -1 && pageSize > 0)
                sqlQuery.Page(pageIndex, pageSize);

            return repo.Query(sqlQuery);
        }

        public static List<TEntity> Query<TEntity>(this ILinqRepository repo, Expression<Func<TEntity, bool>> whereExpression = null)
        {
            var sqlQuery = new LinqSqlQueryBuilder<TEntity>();

            if (whereExpression != null)
                sqlQuery.Where(whereExpression);

            return repo.Query(sqlQuery).Items;
        }

        public static TEntity FirstOrDefault<TEntity>(this ILinqRepository repo, Expression<Func<TEntity, bool>> whereExpression = null)
        {
            return repo.Query(whereExpression, 0, 1).Items.FirstOrDefault();
        }
    }
}