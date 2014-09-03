using System.Collections.Generic;

namespace Taga.Core.Repository
{
    public interface IRepository
    {
        void Save<T>(T entity) where T : class, new();

        T Get<T>(ISql sql);

        T Scalar<T>(ISql sql);

        List<T> Select<T>(ISql sql) where T : class, new();

        IPage<T> Page<T>(int pageIndex, int pageSize, ISql sql);
    }
}
