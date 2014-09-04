using System.Collections.Generic;

namespace Taga.Core.Repository.Linq
{
    public class Sql
    {
        public Sql() {
            Parameters = new List<object>();
        }

        public string Where { get; set; }
        public string OrderBy { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<object> Parameters { get; set; }
    }
}