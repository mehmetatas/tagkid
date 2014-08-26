using System;
using System.Collections.Generic;

namespace Taga.Core.Repository
{
    public interface IRepository : IDisposable
    {
        void Save<T>(T entity) where T : class, new();

        T Get<T>(ISql sql);

        T Scalar<T>(ISql sql);

        List<T> Select<T>(ISql sql) where T : class, new();

        IPage<T> Page<T>(int page, int pageSize, ISql sql);
    }
}
