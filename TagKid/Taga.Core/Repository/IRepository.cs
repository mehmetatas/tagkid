using System.Collections.Generic;
using System.Linq;

namespace Taga.Core.Repository
{
    public interface IRepository
    {
        void Save<T>(T entity) where T : class;
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