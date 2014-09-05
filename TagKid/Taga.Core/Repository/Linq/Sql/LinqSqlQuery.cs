using System.Collections.Generic;
using Taga.Core.Repository.Sql;

namespace Taga.Core.Repository.Linq.Sql
{
    public class LinqSqlQuery : ILinqSqlQuery
    {
        public LinqSqlQuery()
        {
            Parameters = new List<object>();
        }

        internal string Where { get; set; }
        internal string OrderBy { get; set; }
        internal List<object> Parameters { get; private set; }

        public int PageIndex { get; internal set; }
        public int PageSize { get; internal set; }
        public string Query { get; internal set; }

        object[] ISql.Parameters
        {
            get { return Parameters.ToArray(); }
        }
    }
}