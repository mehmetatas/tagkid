using System;
using System.Linq;

namespace TagKid.Framework.Repository.Impl
{
    public class NHRepository : IRepository
    {
        protected static INHSession GetSession(bool openTransaction)
        {
            return NHUnitOfWork.Current.GetSession(openTransaction);
        }

        public virtual void Insert<T>(T entity) where T : class, new()
        {
            GetSession(true).Insert(entity);
        }

        public virtual void Update<T>(T entity) where T : class, new()
        {
            GetSession(true).Update(entity);
        }

        public virtual void Delete<T>(T entity) where T : class, new()
        {
            GetSession(true).Delete(entity);
        }

        public virtual IQueryable<T> Select<T>() where T : class, new()
        {
            return GetSession(false).Query<T>();
        }
    }
}
