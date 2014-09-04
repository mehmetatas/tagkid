using System;
using System.Linq.Expressions;

namespace Taga.Core.Repository.Linq
{
    public interface ILinqQueryBuilder<TEntity>
    {
        ILinqQueryBuilder<TEntity> Where(Expression<Func<TEntity, bool>> expression);
        ILinqQueryBuilder<TEntity> Page(int pageIndex, int pageSize);
        ILinqQueryBuilder<TEntity> OrderBy(Expression<Func<TEntity, dynamic>> expression, bool desc = false);

        ILinqQuery Build();
    }
}