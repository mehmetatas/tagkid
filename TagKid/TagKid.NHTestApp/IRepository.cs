using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NHibernate;
using NHibernate.Linq;

namespace TagKid.NHTestApp
{
    public interface IRepository
    {
        void Insert<T>(T entity) where T : class, new();
        void Update<T>(T entity) where T : class, new();
        void Delete<T>(T entity) where T : class, new();
        IQueryable<T> Select<T>() where T : class, new();
    }

    public class NHRepository : IRepository, IDisposable
    {
        private readonly IStatelessSession _session;

        public NHRepository(ISessionFactory factory)
        {
            _session = factory.OpenStatelessSession();
            _session.Transaction.Begin();
        }

        public void Insert<T>(T entity) where T : class, new()
        {
            _session.Insert(entity);
        }

        public void Update<T>(T entity) where T : class, new()
        {
            _session.Update(entity);
        }

        public void Delete<T>(T entity) where T : class, new()
        {
            _session.Delete(entity);
        }

        public IQueryable<T> Select<T>() where T : class, new()
        {
            return _session.Query<T>();
        }

        public void Dispose()
        {
            _session.Transaction.Commit();
            _session.Dispose();
        }
    }

    public static class RepositoryExtensions
    {
        public static void Save<T>(this IRepository repository, T entity) where T : class, IEntity, new()
        {
            if (entity.Id == 0L)
            {
                repository.Insert(entity);
            }
            else
            {
                repository.Update(entity);
            }
        }

        public static IQueryable<T> Join<T, TProp>(this IQueryable<T> query, Expression<Func<T, TProp>> propExp)
        {
            return query.Fetch(propExp);
        }

        public static void Fetch<TEntity, TProp>(this IRepository repo, Expression<Func<TEntity, TProp>> propExpression, params TEntity[] entities)
            where TEntity : class, IEntity, new()
        {
            repo.Fetch(propExpression, (IList<TEntity>) entities);
        }

        public static void Fetch<TEntity, TProp>(this IRepository repo, Expression<Func<TEntity, TProp>> propExpression, IList<TEntity> entities)
            where TEntity : class, IEntity, new()
        {
            var fetcher = Fetchers.Get(propExpression);
            fetcher.Fetch(repo, entities);
        }
    }

    public static class Fetchers
    {
        private static readonly IDictionary<string, object> Cache = new /*Concurrent*/Dictionary<string, object>();

        public static IFetcher<T> Get<T, TProp>(Expression<Func<T, TProp>> propExp)
            where T : class, IEntity, new()
        {
            var key = MakeKey(propExp);
            if (!Cache.ContainsKey(key))
            {
                throw new NotSupportedException("No fetcher registered for: " + key);
            }
            return (IFetcher<T>) Cache[key];
        }

        public static void RegisterOneToMany<TParent, TChild>(Expression<Func<TParent, IList<TChild>>> propExp,
            Func<IEnumerable<long>, Expression<Func<TChild, bool>>> childFilter,
            Func<TParent, Func<TChild, bool>> parentFilter,
            Action<TParent, List<TChild>> setChildren)
            where TParent : class, IEntity, new()
            where TChild : class, new()
        {
            RegisterFetcher(propExp, new OneToManyFetcher<TParent, TChild>(childFilter, parentFilter, setChildren));
        }

        public static void RegisterManyToMany<TParent, TChild, TAssoc>(Expression<Func<TParent, IList<TChild>>> propExp,
            Func<IEnumerable<long>, Expression<Func<TAssoc, bool>>> assocFilter,
            Func<Expression<Func<TAssoc, ManyToManyItem<TChild>>>> assocSelect,
            Action<TParent, List<TChild>> setChildren)
            where TParent : class, IEntity, new()
            where TChild : class, IEntity, new()
            where TAssoc : class, new()
        {
            RegisterFetcher(propExp, new ManyToManyFetcher<TParent, TChild, TAssoc>(assocFilter, assocSelect, setChildren));
        }

        private static void RegisterFetcher<TParent, TChild>(Expression<Func<TParent, IList<TChild>>> propExp, IFetcher<TParent> fetcher)
            where TParent : class, IEntity, new()
        {
            var key = MakeKey(propExp);
            Cache.Add(key, fetcher);
        }

        private static string MakeKey<T, TProp>(Expression<Func<T, TProp>> propExp)
        {
            MemberExpression memberExp;

            var unaryExpression = propExp.Body as UnaryExpression;
            if (unaryExpression == null)
            {
                memberExp = (MemberExpression)propExp.Body;
            }
            else
            {
                memberExp = (MemberExpression)unaryExpression.Operand;
            }

            return typeof(T).FullName + "." + memberExp.Member.Name;
        }
    }

    public interface IFetcher<T> where T : class, IEntity, new()
    {
        void Fetch(IRepository repo, IList<T> entities);
    }
}
