using System.Data;
using Taga.Core.Repository;

namespace TagKid.Lib.Database
{
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
}