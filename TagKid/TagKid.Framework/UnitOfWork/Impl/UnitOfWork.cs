using System;
using System.Collections.Generic;
using System.Data;
using DummyOrm.Db;
using TagKid.Framework.Context;

namespace TagKid.Framework.UnitOfWork.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _factory;

        private bool _isTransactional;
        private IsolationLevel _isolationLevel;

        private IDb _db;
        internal IDb Db => _db ?? (_db = _factory.Create());

        public UnitOfWork(IDbFactory factory)
        {
            _factory = factory;
            Push(this);
        }

        internal void EnsureTransaction()
        {
            if (_isTransactional)
            {
                Db.BeginTransaction(_isolationLevel);
            }
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _isTransactional = true;
            _isolationLevel = isolationLevel;
        }

        public void Rollback()
        {
            _db?.Rollback();
        }

        public void Commit()
        {
            _db?.Commit();
        }

        public void Dispose()
        {
            Pop();
            _db?.Dispose();
        }

        #region Stack

        private static Stack<UnitOfWork> Stack
        {
            get
            {
                var uowStack = CallContext.Current.Get<Stack<UnitOfWork>>("UnitOfWorkStack");
                if (uowStack == null)
                {
                    uowStack = new Stack<UnitOfWork>();
                    CallContext.Current["UnitOfWorkStack"] = uowStack;
                }
                return uowStack;
            }
        }

        internal static UnitOfWork Current
        {
            get
            {
                if (Stack.Count == 0)
                {
                    throw new InvalidOperationException("No UnitOfWork is available!");
                }
                return Stack.Peek();
            }
        }

        private static void Push(UnitOfWork uow)
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
