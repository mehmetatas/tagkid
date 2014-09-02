using System.Collections.Generic;
using Taga.Core.Repository;

namespace TagKid.Lib.PetaPoco.Repository
{
    class PetaPocoPage<T> : IPage<T>
    {
        private readonly Page<T> _page;

        public PetaPocoPage(Page<T> petaPocoPage)
        {
            _page = petaPocoPage;
        }

        public long CurrentPage
        {
            get { return _page.CurrentPage; }
        }

        public long TotalPages
        {
            get { return _page.TotalPages; }
        }

        public long TotalCount
        {
            get { return _page.TotalItems; }
        }

        public long PageSize
        {
            get { return _page.ItemsPerPage; }
        }

        public List<T> Items
        {
            get { return _page.Items; }
        }
    }
}
