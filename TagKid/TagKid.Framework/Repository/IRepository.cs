using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using TagKid.Framework.Repository.Fetching;
using TagKid.Framework.Repository.Mapping;
using TagKid.Framework.Utils;

namespace TagKid.Framework.Repository
{
    public interface IRepository
    {
        void Insert<T>(T entity) where T : class, new();

        void Update<T>(T entity) where T : class, new();

        void Delete<T>(T entity) where T : class, new();

        IQueryable<T> Select<T>() where T : class, new();

        T Get<T>(object id) where T : class, new();
    }

    public static class RepositoryExtensions
    {
        public static void Save<T>(this IRepository repo, T entity) where T : class, IEntity, new()
        {
            var isNew = entity.Id == 0L;

            if (isNew)
            {
                repo.Insert(entity);
            }
            else
            {
                repo.Update(entity);
            }
        }

        public static void Delete<TEntity, TProp>(this IAdoRepository repo, Expression<Func<TEntity, TProp>> propExpression, params TProp[] values)
            where TEntity : class, new()
        {
            var propInf = propExpression.GetPropertyInfo();

            var mappingProv = MappingProvider.Instance;

            var tableMapping = mappingProv.GetTableMapping<TEntity>();

            var columnMapping = tableMapping.Columns.First(cm => cm.PropertyInfo == propInf);

            var paramNames = Enumerable.Range(0, values.Length).Select(i => String.Format("p_{0}", i)).ToArray();

            var sql = new StringBuilder("DELETE FROM ")
                .Append(tableMapping.TableName)
                .Append(" WHERE ")
                .Append(columnMapping.ColumnName)
                .Append(" IN (")
                .Append(String.Join(",", paramNames.Select(p => String.Format("@{0}", p))))
                .Append(")")
                .ToString();

            var args = Enumerable.Range(0, values.Length).ToDictionary(i => paramNames[i], i => (object)values[i]);

            repo.ExecuteNonQuery(sql, args);
        }

        public static void Fetch<TEntity, TProp>(this IRepository repo, Expression<Func<TEntity, TProp>> propExpression, params TEntity[] entities)
            where TEntity : class, IEntity, new()
        {
            repo.Fetch(entities, propExpression);
        }

        public static void Fetch<TEntity, TProp>(this IRepository repo, IList<TEntity> entities, Expression<Func<TEntity, TProp>> propExpression)
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
            return (IFetcher<T>)Cache[key];
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
            var propInf = propExp.GetPropertyInfo();
            return typeof(T).FullName + "." + propInf.Name;
        }
    }

    public interface IFetcher<T> where T : class, IEntity, new()
    {
        void Fetch(IRepository repo, IList<T> entities);
    }
}
