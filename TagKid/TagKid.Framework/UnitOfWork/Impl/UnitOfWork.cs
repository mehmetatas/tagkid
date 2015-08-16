using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using DummyOrm.Db;
using DummyOrm.Sql.Command;
using TagKid.Framework.Context;

namespace TagKid.Framework.UnitOfWork.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _factory;

        private bool _isTransactional;
        private IsolationLevel _isolationLevel;

        private IDb _db;
        internal IDb Db
        {
            get { return _db ?? (_db = _factory.Create()); }
        }

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
            if (_db != null)
            {
                _db.Rollback();
            }
        }

        public void Commit()
        {
            if (_db != null)
            {
                _db.Commit();
            }
        }

        public void Dispose()
        {
            Pop();
            if (_db != null)
            {
                _db.Dispose();
            }
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

    public class Repository : IRepository
    {
        private static IDb Db
        {
            get
            {
                return UnitOfWork.Current.Db;
            }
        }

        private void EnsureTransaction()
        {
            UnitOfWork.Current.EnsureTransaction();
        }

        public void Insert<T>(T entity) where T : class, new()
        {
            EnsureTransaction();
            Db.Insert(entity);
        }

        public void Update<T>(T entity) where T : class, new()
        {
            EnsureTransaction();
            Db.Update(entity);
        }

        public void Delete<T>(T entity) where T : class, new()
        {
            EnsureTransaction();
            Db.Delete(entity);
        }

        public void DeleteMany<T>(Expression<Func<T, bool>> filter) where T : class, new()
        {
            EnsureTransaction();
            Db.DeleteMany(filter);
        }

        public T GetById<T>(object id) where T : class, new()
        {
            return Db.GetById<T>(id);
        }

        public IQuery<T> Select<T>() where T : class, new()
        {
            return Db.Select<T>();
        }

        public void Load<T, TProp>(IList<T> parentEntities, Expression<Func<T, TProp>> childPropExp, Expression<Func<TProp, object>> includeChildProps = null)
            where T : class, new()
            where TProp : class, new()
        {
            Db.Load(parentEntities, childPropExp, includeChildProps);
        }

        public void LoadMany<T, TProp>(IList<T> parentEntities, Expression<Func<T, IList<TProp>>> childrenListExp, Expression<Func<TProp, object>> includeChildProps = null)
            where T : class, new()
            where TProp : class, new()
        {
            Db.LoadMany(parentEntities, childrenListExp, includeChildProps);
        }
    }

    public class AdoRepository : IAdoRepository
    {
        private static IDb Db
        {
            get { return UnitOfWork.Current.Db; }
        }

        private void EnsureTransaction()
        {
            UnitOfWork.Current.EnsureTransaction();
        }

        public IList<T> ExecuteQuery<T>(Command cmd) where T : class, new()
        {
            return Db.ExecuteQuery<T>(cmd);
        }

        public int ExecuteNonQuery(Command cmd)
        {
            EnsureTransaction();
            return Db.ExecuteNonQuery(cmd);
        }

        public object ExecuteScalar(Command cmd)
        {
            EnsureTransaction();
            return Db.ExecuteScalar(cmd);
        }
    }
}
