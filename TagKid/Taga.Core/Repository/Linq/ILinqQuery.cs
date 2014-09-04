using System;
using System.Linq.Expressions;

namespace Taga.Core.Repository.Linq
{
    public interface ILinqQuery<TEntity>
    {
        ILinqQuery<TEntity> Where(Expression<Func<TEntity, bool>> expression);
        ILinqQuery<TEntity> Page(int pageIndex, int pageSize);
        ILinqQuery<TEntity> OrderBy<TProp>(Expression<Func<TEntity, TProp>> expression, bool desc = false);

        IPage<TEntity> Select(ICrudRepository repository);
    }
}