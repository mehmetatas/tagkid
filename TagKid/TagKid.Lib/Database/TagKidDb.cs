using System.Collections;
using System.Data;
using Taga.Core.IoC;
using Taga.Core.Repository;
using TagKid.Lib.Repositories;

namespace TagKid.Lib.Database
{
    public class TagKidDb : ITransactionalUnitOfWork, IRepositoryProvider
    {
        private readonly Hashtable _repositories;
        private readonly ITransactionalUnitOfWork _unitOfWork;

        public TagKidDb()
        {
            _unitOfWork = ServiceProvider.Provider.GetOrCreate<ITransactionalUnitOfWork>();
            _repositories = new Hashtable();
        }

        public virtual TRepository GetRepository<TRepository>() where TRepository : ITagKidRepository
        {
            var type = typeof (TRepository);
            if (_repositories.Contains(type))
                return (TRepository) _repositories[type];

            var repository = ServiceProvider.Provider.GetOrCreate<TRepository>();
            _repositories.Add(type, repository);
            return repository;
        }

        public virtual void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _unitOfWork.BeginTransaction(isolationLevel);
        }

        public virtual void Save()
        {
            _unitOfWork.Save();
        }

        public virtual void RollbackTransaction()
        {
            _unitOfWork.RollbackTransaction();
        }

        public virtual void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}