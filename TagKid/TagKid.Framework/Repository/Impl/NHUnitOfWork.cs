using NHibernate;
using System;
using System.Collections.Generic;
using System.Data;
using TagKid.Framework.Context;

namespace TagKid.Framework.Repository.Impl
{
    public class NHUnitOfWork : IUnitOfWork
    {
        private readonly ISessionFactory _sessionFactory;

        private bool _disposed;

        private bool _beginTransactionOnSessionOpen;

        private ITransaction _transaction;

        private INHSession _session;

        internal INHSession GetSession(bool openTransaction)
        {
            if (_session == null)
            {
                _session = NHSession.Stateful(_sessionFactory);
            }

            if (_beginTransactionOnSessionOpen && openTransaction)
            {
                DoBeginTransaction();
            }

            return _session;
        }

        public NHUnitOfWork(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
            Push(this);
        }

        public void BeginTransaction()
        {
            if (_session == null)
            {
                _beginTransactionOnSessionOpen = true;
            }
            else if (_transaction == null)
            {
                DoBeginTransaction();
            }
        }

        private void DoBeginTransaction()
        {
            if (_transaction == null)
            {
                _transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted);
            }
        }

        public void Commit()
        {
            if (_transaction == null || !_transaction.IsActive)
            {
                return;
            }
            _session.Flush();
            _transaction.Commit();
            _transaction.Dispose();
            _transaction = null;
        }

        public void Rollback()
        {
            if (_transaction == null || !_transaction.IsActive)
            {
                return;
            }
            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            
            _disposed = true;
            
            Pop();
            
            if (_session == null || !_session.IsOpen)
            {
                return;
            }
            
            // Default dispose behavior is rollback. 
            // If transaction is already committed or there is no transaction, rollback will do nothing.
            Rollback();

            _session.Close();
            _session.Dispose();
            _session = null;
        }

        #region Stack

        private static Stack<NHUnitOfWork> Stack
        {
            get
            {
                var uowStack = CallContext.Current.Get<Stack<NHUnitOfWork>>("NHUnitOfWorkStack");
                if (uowStack == null)
                {
                    uowStack = new Stack<NHUnitOfWork>();
                    CallContext.Current["NHUnitOfWorkStack"] = uowStack;
                }
                return uowStack;
            }
        }

        internal static NHUnitOfWork Current
        {
            get
            {
                if (Stack.Count == 0)
                {
                    throw new InvalidOperationException("No NHUnitOfWork is available!");
                }
                return Stack.Peek();
            }
        }

        private static void Push(NHUnitOfWork uow)
        {
            Stack.Push(uow);
        }

        private static void Pop()
        {
            Stack.Pop();
        }

        #endregion
    }
}