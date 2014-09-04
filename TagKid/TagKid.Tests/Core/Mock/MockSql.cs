using Taga.Core.Repository.Sql;

namespace TagKid.Tests.Core.Mock
{
    public class MockSql : ISql
    {
        public string Query { get; set; }

        public object[] Parameters { get; set; }
    }
}
