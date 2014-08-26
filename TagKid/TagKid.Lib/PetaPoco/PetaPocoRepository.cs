using System.Collections.Generic;
using Taga.Core.DynamicProxy;
using Taga.Core.Repository;

namespace TagKid.Lib.PetaPoco
{
    [Intercept]
    class PetaPocoRepository : IRepository
    {
        private readonly Database _db;
        private bool _useTransaction;

        private PetaPocoRepository(bool useTransaction)
        {
            _db = new Database("tagkid");
            if (useTransaction)
                BeginTransaction();
        }

        protected virtual void BeginTransaction()
        {
            _db.BeginTransaction();
            _useTransaction = true;
        }

        protected virtual void Rollback()
        {
            if (_useTransaction)
                _db.AbortTransaction();
            _useTransaction = false;
        }

        protected virtual void CommitTransaction()
        {
            if (_useTransaction)
                _db.CompleteTransaction();
        }

        public virtual void Save<T>(T entity) where T : class, new()
        {
            try
            {
                _db.Save(entity);
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        public virtual T Get<T>(ISql sql)
        {
            try
            {
                var ppSql = (PetaPocoSqlAdapter)sql;
                return _db.SingleOrDefault<T>(ppSql.Sql);
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        public virtual T Scalar<T>(ISql sql)
        {
            try
            {
                var ppSql = (PetaPocoSqlAdapter) sql;
                return _db.ExecuteScalar<T>(ppSql.Sql);
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        public virtual List<T> Select<T>(ISql sql) where T : class, new()
        {
            try
            {
                var ppSql = (PetaPocoSqlAdapter)sql;
                return _db.Fetch<T>(ppSql.Sql);
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        public virtual IPage<T> Page<T>(int page, int pageSize, ISql sql)
        {
            try
            {
                var ppSql = (PetaPocoSqlAdapter)sql;
                var petaPocoPage = _db.Page<T>(page, pageSize, ppSql.Sql);
                return new PetaPocoPageAdapter<T>(petaPocoPage);
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        public virtual void Dispose()
        {
            CommitTransaction();
            _db.Dispose();
        }

        public static PetaPocoRepository New(bool useTransaction)
        {
            return new PetaPocoRepository(useTransaction);
        }
    }
}
