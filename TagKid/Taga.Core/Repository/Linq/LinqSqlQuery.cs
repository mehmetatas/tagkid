using System;
using System.Linq.Expressions;

namespace Taga.Core.Repository.Linq
{
    public class LinqSqlQuery<TEntity> : ILinqQuery<TEntity>, ILinqQueryBuilder<Sql>
    {
        private Sql _sql = new Sql();

        public ILinqQuery<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            var visitor = new SqlExpressionVisitor();
            _sql = visitor.ToSql(expression);
            return this;
        }

        public ILinqQuery<TEntity> Page(int pageIndex, int pageSize)
        {
            _sql.PageIndex = pageIndex;
            _sql.PageSize = pageSize;
            return this;
        }

        public ILinqQuery<TEntity> OrderBy<TProp>(Expression<Func<TEntity, TProp>> expression, bool desc)
        {
            var memberExpression = (MemberExpression)expression.Body;

            var order = desc ? "DESC" : "ASC";

            _sql.OrderBy = String.IsNullOrEmpty(_sql.OrderBy)
                               ? String.Format("{0} {1}", memberExpression.Member.Name, order)
                               : String.Format("{0}, {1} {2}", _sql.OrderBy, memberExpression.Member.Name, order);

            return this;
        }

        public IPage<TEntity> Select(ICrudRepository repository)
        {
            return repository.Select(this);
        }

        public Sql Build()
        {
            return _sql;
        }
    }
}