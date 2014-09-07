using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Taga.Core.IoC;

namespace Taga.Core.Repository.Linq.Sql
{
    public class LinqSqlQueryBuilder<TEntity> : ILinqQueryBuilder<TEntity>
    {
        private LinqSqlQuery _sql = new LinqSqlQuery();
        private readonly ILinqSqlSchemaSolver _sqlSchemaSolver;

        public LinqSqlQueryBuilder()
        {
            _sqlSchemaSolver = ServiceProvider.Provider.GetOrCreate<ILinqSqlSchemaSolver>();
        }

        public ILinqQueryBuilder<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            var visitor = new LinqSqlExpressionVisitor(_sqlSchemaSolver);
            _sql = visitor.ToSql(expression);
            return this;
        }

        public ILinqQueryBuilder<TEntity> Page(int pageIndex, int pageSize)
        {
            _sql.PageIndex = pageIndex;
            _sql.PageSize = pageSize;
            return this;
        }

        public ILinqQueryBuilder<TEntity> OrderBy(Expression<Func<TEntity, dynamic>> expression, bool desc)
        {
            var memberExpression = (MemberExpression)((UnaryExpression)expression.Body).Operand;

            var order = desc ? "DESC" : "ASC";

            _sql.OrderBy = String.IsNullOrEmpty(_sql.OrderBy)
                               ? String.Format("{0} {1}", GetColumnName(memberExpression), order)
                               : String.Format("{0}, {1} {2}", _sql.OrderBy, GetColumnName(memberExpression), order);

            return this;
        }

        private string GetColumnName(MemberExpression exp)
        {
            return _sqlSchemaSolver.GetColumnName((PropertyInfo) exp.Member);
        }

        public ILinqQuery Build()
        {
            var tableName = _sqlSchemaSolver.GetTableName(typeof(TEntity));
            var sql = new StringBuilder();

            sql.Append("SELECT * FROM ");
            sql.Append(tableName);

            if (!String.IsNullOrWhiteSpace(_sql.Where))
            {
                sql.Append(" WHERE ");
                sql.Append(_sql.Where);
            }

            if (!String.IsNullOrWhiteSpace(_sql.OrderBy))
            {
                sql.Append(" ORDER BY ");
                sql.Append(_sql.OrderBy);
            }

            _sql.Query = sql.ToString();

            return _sql;
        }
    }
}