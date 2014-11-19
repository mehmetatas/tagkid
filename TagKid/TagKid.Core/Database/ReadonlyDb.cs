using System.Collections;
using Taga.Core.IoC;
using Taga.Core.Repository;
using TagKid.Core.Repository;

namespace TagKid.Core.Database
{
    class ReadonlyDb : IReadonlyDb
    {
        private readonly Hashtable _repositories;
        protected readonly ITransactionalUnitOfWork UnitOfWork;

        public ReadonlyDb(ITransactionalUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            _repositories = new Hashtable();
        }

        public virtual void Dispose()
        {
            UnitOfWork.Dispose();
            _repositories.Clear();
        }

        public TRepository GetRepository<TRepository>() where TRepository : ITagKidRepository
        {
            return ServiceProvider.Provider.GetOrCreate<TRepository>();
        }
    }
}