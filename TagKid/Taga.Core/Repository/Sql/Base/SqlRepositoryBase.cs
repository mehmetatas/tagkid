using System.Collections.Generic;
using Taga.Core.Repository.Base;

namespace Taga.Core.Repository.Sql.Base
{
    public abstract class SqlRepositoryBase : ISqlRepository
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected SqlRepositoryBase()
        {
            UnitOfWork = UnitOfWorkBase.Current;
        }

        public abstract void Save<T>(T entity) where T : class, new();

        public abstract void Delete<T>(T entity) where T : class, new();

        public abstract T Get<T>(ISql sql);

        public abstract T Scalar<T>(ISql sql);

        public abstract List<T> Select<T>(ISql sql) where T : class, new();

        public abstract IPage<T> Page<T>(int page, int pageSize, ISql sql);
    }
}
