using System.Data;
using System.Data.Entity;
using Taga.Core.Repository;
using Taga.Core.Repository.Base;

namespace Taga.Repository.EF
{
    public class EFUnitOfWork : UnitOfWork, ITransactionalUnitOfWork, IEFUnitOfWork
    {
        private readonly DbContext _dbContext;
        private DbContextTransaction _transaction;

        public EFUnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        DbContext IEFUnitOfWork.DbContext
        {
            get { return _dbContext; }
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _transaction = _dbContext.Database.BeginTransaction(isolationLevel);
        }

        public override void Save()
        {
            _dbContext.SaveChanges();

            if (_transaction == null)
                return;

            _transaction.Commit();
            _transaction = null;
        }

        public void RollbackTransaction()
        {
            if (_transaction == null) 
                return;

            _transaction.Rollback();
            _transaction = null;
        }

        public void Dispose()
        {
            RollbackTransaction();
        }
    }
}
