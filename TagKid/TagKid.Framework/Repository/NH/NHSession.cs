using System;
using System.Data;
using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace TagKid.Framework.Repository.NH
{
    static class NHSession
    {
        public static INHSession Stateful(ISessionFactory sessionFactory)
        {
            return new StatefulSession(sessionFactory.OpenSession());
        }

        public static INHSession Stateless(ISessionFactory sessionFactory)
        {
            return new StatelessSession(sessionFactory.OpenStatelessSession());
        }
    }

    public interface INHSession : IDisposable
    {
        bool IsOpen { get; }
        ITransaction BeginTransaction(IsolationLevel isolationLevel);
        void Insert(object entity);
        void Update(object entity);
        void Delete(object entity);
        IQueryable<T> Query<T>();
        T Get<T>(object id);
        IDbCommand CreateCommand();
        void Flush();
        void Close();
    }

    class StatelessSession : INHSession
    {
        private readonly IStatelessSession _session;

        public StatelessSession(IStatelessSession session)
        {
            _session = session;
        }

        public bool IsOpen
        {
            get { return _session.IsOpen; }
        }

        public ITransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return _session.BeginTransaction(isolationLevel);
        }

        public void Insert(object entity)
        {
            _session.Insert(entity);
        }

        public void Update(object entity)
        {
            _session.Update(entity);
        }

        public void Delete(object entity)
        {
            _session.Delete(entity);
        }

        public IQueryable<T> Query<T>()
        {
            return _session.Query<T>();
        }

        public T Get<T>(object id)
        {
            return _session.Get<T>(id);
        }

        public IDbCommand CreateCommand()
        {
            var cmd = _session.Connection.CreateCommand();

            if (_session.Transaction.IsActive)
            {
                _session.Transaction.Enlist(cmd);
            }

            return cmd;
        }

        public void Flush()
        {

        }

        public void Close()
        {
            _session.Close();
        }

        public void Dispose()
        {
            _session.Dispose();
        }
    }

    class StatefulSession : INHSession
    {
        private readonly ISession _session;

        public StatefulSession(ISession session)
        {
            _session = session;
            _session.FlushMode = FlushMode.Auto;
        }

        public bool IsOpen
        {
            get { return _session.IsOpen; }
        }

        public ITransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return _session.BeginTransaction(isolationLevel);
        }

        public void Insert(object entity)
        {
            _session.Save(entity);
        }

        public void Update(object entity)
        {
            entity = _session.Merge(entity);
            _session.Update(entity);
        }

        public void Delete(object entity)
        {
            _session.Delete(entity);
        }

        public IQueryable<T> Query<T>()
        {
            return _session.Query<T>();
        }

        public T Get<T>(object id)
        {
            return _session.Get<T>(id);
        }

        public IDbCommand CreateCommand()
        {
            var cmd = _session.Connection.CreateCommand();

            if (_session.Transaction.IsActive)
            {
                _session.Transaction.Enlist(cmd);
            }

            return cmd;
        }

        public void Flush()
        {
            _session.Flush();
        }

        public void Close()
        {
            _session.Close();
        }

        public void Dispose()
        {
            _session.Dispose();
        }
    }
}