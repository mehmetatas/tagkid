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

        public abstract void Save<T>(T entity);

        public abstract void Delete<T>(T entity);

        public abstract IPage<T> ExecuteQuery<T>(ISql sql, int pageIndex = 1, int pageSize = 1000);

        public abstract int ExecuteNonQuery<T>(ISql sql);
    }
}
