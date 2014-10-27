using System;
using System.Collections;
using System.Data;
using Taga.Core.IoC;
using Taga.Core.Repository;
using Taga.UserApp.Core.Repository;

namespace Taga.UserApp.Core.Database
{
    public static class Db
    {
        private static ITransactionalUnitOfWork CreateUnitOfWork()
        {
            return ServiceProvider.Provider.GetOrCreate<ITransactionalUnitOfWork>();
        }

        public static IReadonlyDb Readonly()
        {
            return new ReadonlyDb(CreateUnitOfWork());
        }

        public static IReadWriteDb ReadWrite()
        {
            return new ReadWriteDb(CreateUnitOfWork());
        }

        public static ITransactionalDb Transactional()
        {
            return new TransactionalReadWriteDb(CreateUnitOfWork());
        }
    }

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

        public TRepository GetRepository<TRepository>() where TRepository : IUserAppRepository
        {
            var type = typeof(TRepository);
            if (_repositories.Contains(type))
                return (TRepository)_repositories[type];

            var repository = ServiceProvider.Provider.GetOrCreate<TRepository>();
            _repositories.Add(type, repository);
            return repository;
        }
    }

    class ReadWriteDb : ReadonlyDb, IReadWriteDb
    {
        public ReadWriteDb(ITransactionalUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public virtual void Save()
        {
            UnitOfWork.Save();
        }
    }

    class TransactionalReadWriteDb : ReadWriteDb, ITransactionalDb
    {
        public TransactionalReadWriteDb(ITransactionalUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            UnitOfWork.BeginTransaction(isolationLevel);
        }

        public void RollbackTransaction()
        {
            UnitOfWork.RollbackTransaction();
        }
    }

    public interface IReadonlyDb : IRepositoryProvider, IDisposable
    {
    }

    public interface IReadWriteDb : IReadonlyDb, IUnitOfWork
    {
    }

    public interface ITransactionalDb : IReadWriteDb, ITransactionalUnitOfWork
    {
    }
}
