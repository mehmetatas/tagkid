using System.Collections.Generic;
using System.Linq;
using Taga.Core.IoC;
using Taga.Core.Repository.Mapping;

namespace Taga.Core.Repository
{
    public interface IRepository
    {
        void Insert<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        IQueryable<T> Select<T>() where T : class;

        IList<T> Exec<T>(string spNameOrSql, IDictionary<string, object> args = null, bool rawSql = false)
            where T : class;
    }

    internal class Page<T> : IPage<T>
    {
        public long CurrentPage { get; set; }

        public long PageSize { get; set; }

        public long TotalPages { get; set; }

        public long TotalCount { get; set; }

        public List<T> Items { get; set; }
    }

    public static class RepositoryExtensions
    {
        public static void Save<T>(this IRepository repo, T entity) where T : class
        {
            var mapingProv = ServiceProvider.Provider.GetOrCreate<IMappingProvider>();

            var tableMapping = mapingProv.GetTableMapping<T>();

            if (tableMapping.IdColumns.Length != 1 || !tableMapping.IdColumns[0].IsAutoIncrement)
            {
                throw new SaveException();
            }

            var isNew = tableMapping.IdColumns[0].PropertyInfo.GetValue(entity).Equals(0L);

            if (isNew)
            {
                repo.Insert(entity);
            }
            else
            {
                repo.Update(entity);
            }
        }

        public static IList<T> ExecSp<T>(this IRepository repo, string spName, IDictionary<string, object> args = null)
            where T : class
        {
            return repo.Exec<T>(spName, args);
        }

        public static IList<T> ExecSql<T>(this IRepository repo, string sql, IDictionary<string, object> args = null)
            where T : class
        {
            return repo.Exec<T>(sql, args, true);
        }

        public static IPage<T> Page<T>(this IQueryable<T> query, int pageIndex, int pageSize) where T : class
        {
            var totalCount = query.Count();

            var pageCount = (totalCount - 1)/pageSize + 1;

            var items = query.Skip((pageIndex - 1)*pageSize).Take(pageSize).ToList();

            return new Page<T>
            {
                PageSize = pageSize,
                CurrentPage = pageIndex,
                TotalCount = totalCount,
                TotalPages = pageCount,
                Items = items.ToList()
            };
        }
    }
}