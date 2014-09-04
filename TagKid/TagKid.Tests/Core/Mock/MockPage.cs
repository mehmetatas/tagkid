using System.Collections.Generic;
using Taga.Core.Repository;

namespace TagKid.Tests.Core.Mock
{
    class MockPage<T> : IPage<T>
    {
        public long CurrentPage { get; set; }

        public long TotalPages { get; set; }

        public long TotalCount { get; set; }

        public long PageSize { get; set; }

        public List<T> Items { get; set; }
    }
}
