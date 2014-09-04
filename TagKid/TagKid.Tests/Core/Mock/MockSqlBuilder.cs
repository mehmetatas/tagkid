using Taga.Core.Repository.Sql;
using Taga.Core.Repository.Sql.Base;

namespace TagKid.Tests.Core.Mock
{
    public class MockSqlBuilder : SqlBuilder
    {
        protected override ISql BuildSql(string sql, object[] parameters)
        {
            return new MockSql { Query = sql, Parameters = parameters };
        }
    }
}
