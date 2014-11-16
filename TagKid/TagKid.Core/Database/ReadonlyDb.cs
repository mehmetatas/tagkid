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
            var type = typeof(TRepository);
            if (_repositories.Contains(type))
                return (TRepository)_repositories[type];

            var repository = ServiceProvider.Provider.GetOrCreate<TRepository>();
            _repositories.Add(type, repository);
            return repository;
        }
    }
}