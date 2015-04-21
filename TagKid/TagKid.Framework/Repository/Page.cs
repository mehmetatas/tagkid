using System.Collections.Generic;
using System.Linq;

namespace TagKid.Framework.Repository
{
    public class Page<T>
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public int TotalCount { get; set; }

        public List<T> Items { get; set; }
    }

    public static class PagingExtensions
    {
        public static Page<T> Page<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            var totalCount = query.Count();

            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }
            
            var pageCount = (totalCount - 1) / pageSize + 1;
            var items = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new Page<T>
            {
                PageSize = pageSize,
                CurrentPage = pageIndex,
                TotalCount = totalCount,
                TotalPages = pageCount,
                Items = items
            };
        }
    }
}