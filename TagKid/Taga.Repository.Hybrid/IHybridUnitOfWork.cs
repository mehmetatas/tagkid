using Taga.Core.Repository;

namespace Taga.Repository.Hybrid
{
    interface IHybridUnitOfWork
    {
        void Insert(object entity);

        void Update(object entity);

        void Delete(object entity);

        IQueryProvider QueryProvider { get; }
    }
}