using NHibernate;
using System.Data;
using Taga.Core.Repository;
using Taga.Core.Repository.Base;

namespace Taga.Repository.NH
{
    public class NHUnitOfWork : UnitOfWork, ITransactionalUnitOfWork, INHUnitOfWork
    {
        private readonly ISession _session;
        private ITransaction _transaction;

        public NHUnitOfWork(ISessionFactory sessionFactory)
        {
            _session = sessionFactory.OpenSession();
        }

        ISession INHUnitOfWork.Session
        {
            get { return _session; }
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _transaction = _session.BeginTransaction(isolationLevel);
        }

        public override void Save()
        {
            _session.Flush();

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

        protected override void OnDispose()
        {
            RollbackTransaction();
            _session.Dispose();
        }
    }
}