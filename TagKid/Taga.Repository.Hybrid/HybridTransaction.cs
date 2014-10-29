using System.Data;
using Taga.Core.Repository;

namespace Taga.Repository.Hybrid
{
    class HybridTransaction : ITransaction
    {
        private readonly IDbTransaction _transaction;

        public HybridTransaction(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }
    }
}