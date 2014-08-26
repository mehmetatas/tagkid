using System.Collections.Generic;

namespace Taga.Core.Repository
{
    public interface IPage<T>
    {
        long CurrentPage { get; }

        long TotalPages { get;}

        long TotalCount { get; }

        long PageSize { get; }

        List<T> Items { get; }
    }
}
