using System.Collections.Generic;

namespace Taga.Core.Repository.Base
{
    public abstract class Repository : IRepository
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected Repository()
        {
            UnitOfWork = Base.UnitOfWork.Current;
        }

        public abstract void Save<T>(T entity) where T : class, new();

        public abstract T Get<T>(ISql sql);

        public abstract T Scalar<T>(ISql sql);

        public abstract List<T> Select<T>(ISql sql) where T : class, new();

        public abstract IPage<T> Page<T>(int page, int pageSize, ISql sql);
    }
}
