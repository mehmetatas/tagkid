using Microsoft.VisualStudio.TestTools.UnitTesting;
using TagKid.Tests.Core.Mock;

namespace TagKid.Tests.Core
{
    [TestClass]
    public class SqlBuilderTests
    {
        [TestMethod]
        public void TestAnd()
        {
            var builder = new MockSqlBuilder();
            builder.And();
            var sql = builder.Build();
            Assert.AreEqual(" AND", sql.Query);
        }

        [TestMethod]
        public void TestAndWithColumn()
        {
            var builder = new MockSqlBuilder();
            builder.And("TEST");
            var sql = builder.Build();
            Assert.AreEqual(" AND TEST", sql.Query);
        }

        [TestMethod]
        public void TestOr()
        {
            var builder = new MockSqlBuilder();
            builder.Or();
            var sql = builder.Build();
            Assert.AreEqual(" OR", sql.Query);
        }

        [TestMethod]
        public void TestOrWithColumn()
        {
            var builder = new MockSqlBuilder();
            builder.Or("TEST");
            var sql = builder.Build();
            Assert.AreEqual(" OR TEST", sql.Query);
        }

        [TestMethod]
        public void TestAppend()
        {
            var builder = new MockSqlBuilder();
            builder.Append("SELECT * FROM TEST WHERE COL = @0", 12);
            var sql = builder.Build();
            Assert.AreEqual("SELECT * FROM TEST WHERE COL = @0", sql.Query);
            Assert.AreEqual(1, sql.Parameters.Length);
            Assert.AreEqual(12, sql.Parameters[0]);
        }

        [TestMethod]
        public void TestBetween()
        {
            var builder = new MockSqlBuilder();
            builder.Between(1, 10);
            var sql = builder.Build();
            Assert.AreEqual(" BETWEEN @0 AND @1", sql.Query);
            Assert.AreEqual(2, sql.Parameters.Length);
            Assert.AreEqual(1, sql.Parameters[0]);
            Assert.AreEqual(10, sql.Parameters[1]);
        }

        [TestMethod]
        public void TestBetweenWithColumn()
        {
            var builder = new MockSqlBuilder();
            builder.Between("TEST", 1, 10);
            var sql = builder.Build();
            Assert.AreEqual(" TEST BETWEEN @0 AND @1", sql.Query);
            Assert.AreEqual(2, sql.Parameters.Length);
            Assert.AreEqual(1, sql.Parameters[0]);
            Assert.AreEqual(10, sql.Parameters[1]);
        }

        [TestMethod]
        public void TestEquals()
        {
            var builder = new MockSqlBuilder();
            builder.Equals();
            var sql = builder.Build();
            Assert.AreEqual(" =", sql.Query);
        }

        [TestMethod]
        public void TestEqualsWithParam()
        {
            var builder = new MockSqlBuilder();
            builder.EqualsParam(12);
            var sql = builder.Build();
            Assert.AreEqual(" = @0", sql.Query);
            Assert.AreEqual(1, sql.Parameters.Length);
            Assert.AreEqual(12, sql.Parameters[0]);
        }

        [TestMethod]
        public void TestEqualsWithColumnAndParam()
        {
            var builder = new MockSqlBuilder();
            builder.Equals("TEST", 12);
            var sql = builder.Build();
            Assert.AreEqual(" TEST = @0", sql.Query);
            Assert.AreEqual(1, sql.Parameters.Length);
            Assert.AreEqual(12, sql.Parameters[0]);
        }

        [TestMethod]
        public void TestNotEquals()
        {
            var builder = new MockSqlBuilder();
            builder.NotEquals();
            var sql = builder.Build();
            Assert.AreEqual(" <>", sql.Query);
        }

        [TestMethod]
        public void TestNotEqualsWithParam()
        {
            var builder = new MockSqlBuilder();
            builder.NotEquals(12);
            var sql = builder.Build();
            Assert.AreEqual(" <> @0", sql.Query);
            Assert.AreEqual(1, sql.Parameters.Length);
            Assert.AreEqual(12, sql.Parameters[0]);
        }

        [TestMethod]
        public void TestNotEqualsWithColumnAndParam()
        {
            var builder = new MockSqlBuilder();
            builder.NotEquals("TEST", 12);
            var sql = builder.Build();
            Assert.AreEqual(" TEST <> @0", sql.Query);
            Assert.AreEqual(1, sql.Parameters.Length);
            Assert.AreEqual(12, sql.Parameters[0]);
        }

        [TestMethod]
        public void TestGreaterThan()
        {
            var builder = new MockSqlBuilder();
            builder.GreaterThan();
            var sql = builder.Build();
            Assert.AreEqual(" >", sql.Query);
        }

        [TestMethod]
        public void TestGreaterThanWithParam()
        {
            var builder = new MockSqlBuilder();
            builder.GreaterThan(12);
            var sql = builder.Build();
            Assert.AreEqual(" > @0", sql.Query);
            Assert.AreEqual(1, sql.Parameters.Length);
            Assert.AreEqual(12, sql.Parameters[0]);
        }

        [TestMethod]
        public void TestGreaterThanWithColumnAndParam()
        {
            var builder = new MockSqlBuilder();
            builder.GreaterThan("TEST", 12);
            var sql = builder.Build();
            Assert.AreEqual(" TEST > @0", sql.Query);
            Assert.AreEqual(1, sql.Parameters.Length);
            Assert.AreEqual(12, sql.Parameters[0]);
        }

        [TestMethod]
        public void TestGreaterThanOrEquals()
        {
            var builder = new MockSqlBuilder();
            builder.GreaterThanOrEquals();
            var sql = builder.Build();
            Assert.AreEqual(" >=", sql.Query);
        }

        [TestMethod]
        public void TestGreaterThanOrEqualsWithParam()
        {
            var builder = new MockSqlBuilder();
            builder.GreaterThanOrEquals(12);
            var sql = builder.Build();
            Assert.AreEqual(" >= @0", sql.Query);
            Assert.AreEqual(1, sql.Parameters.Length);
            Assert.AreEqual(12, sql.Parameters[0]);
        }

        [TestMethod]
        public void TestGreaterThanOrEqualsWithColumnAndParam()
        {
            var builder = new MockSqlBuilder();
            builder.GreaterThanOrEquals("TEST", 12);
            var sql = builder.Build();
            Assert.AreEqual(" TEST >= @0", sql.Query);
            Assert.AreEqual(1, sql.Parameters.Length);
            Assert.AreEqual(12, sql.Parameters[0]);
        }

        [TestMethod]
        public void TestLessThan()
        {
            var builder = new MockSqlBuilder();
            builder.LessThan();
            var sql = builder.Build();
            Assert.AreEqual(" <", sql.Query);
        }

        [TestMethod]
        public void TestLessThanWithParam()
        {
            var builder = new MockSqlBuilder();
            builder.LessThan(12);
            var sql = builder.Build();
            Assert.AreEqual(" < @0", sql.Query);
            Assert.AreEqual(1, sql.Parameters.Length);
            Assert.AreEqual(12, sql.Parameters[0]);
        }

        [TestMethod]
        public void TestLessThanWithColumnAndParam()
        {
            var builder = new MockSqlBuilder();
            builder.LessThan("TEST", 12);
            var sql = builder.Build();
            Assert.AreEqual(" TEST < @0", sql.Query);
            Assert.AreEqual(1, sql.Parameters.Length);
            Assert.AreEqual(12, sql.Parameters[0]);
        }

        [TestMethod]
        public void TestLessThanOrEquals()
        {
            var builder = new MockSqlBuilder();
            builder.LessThanOrEquals();
            var sql = builder.Build();
            Assert.AreEqual(" <=", sql.Query);
        }

        [TestMethod]
        public void TestLessThanOrEqualsWithParam()
        {
            var builder = new MockSqlBuilder();
            builder.LessThanOrEquals(12);
            var sql = builder.Build();
            Assert.AreEqual(" <= @0", sql.Query);
            Assert.AreEqual(1, sql.Parameters.Length);
            Assert.AreEqual(12, sql.Parameters[0]);
        }

        [TestMethod]
        public void TestLessThanOrEqualsWithColumnAndParam()
        {
            var builder = new MockSqlBuilder();
            builder.LessThanOrEquals("TEST", 12);
            var sql = builder.Build();
            Assert.AreEqual(" TEST <= @0", sql.Query);
            Assert.AreEqual(1, sql.Parameters.Length);
            Assert.AreEqual(12, sql.Parameters[0]);
        }

        [TestMethod]
        public void TestParam()
        {
            var builder = new MockSqlBuilder();
            builder.Param(12);
            var sql = builder.Build();
            Assert.AreEqual(" @0", sql.Query);
            Assert.AreEqual(1, sql.Parameters.Length);
            Assert.AreEqual(12, sql.Parameters[0]);
        }

        [TestMethod]
        public void TestColumn()
        {
            var builder = new MockSqlBuilder();
            builder.Column("TEST");
            var sql = builder.Build();
            Assert.AreEqual(" TEST", sql.Query);
        }

        [TestMethod]
        public void TestWhere()
        {
            var builder = new MockSqlBuilder();
            builder.Where("TEST");
            var sql = builder.Build();
            Assert.AreEqual(" WHERE TEST", sql.Query);
        }

        [TestMethod]
        public void TestSelect()
        {
            var builder = new MockSqlBuilder();
            builder.Select("TEST1", "TEST2");
            var sql = builder.Build();
            Assert.AreEqual("SELECT TEST1,TEST2", sql.Query);
        }

        [TestMethod]
        public void TestSelectAllFrom()
        {
            var builder = new MockSqlBuilder();
            builder.SelectAllFrom("TEST");
            var sql = builder.Build();
            Assert.AreEqual("SELECT * FROM TEST", sql.Query);
        }

        [TestMethod]
        public void TestFrom()
        {
            var builder = new MockSqlBuilder();
            builder.From("TEST");
            var sql = builder.Build();
            Assert.AreEqual(" FROM TEST", sql.Query);
        }

        [TestMethod]
        public void TestDeleteFrom()
        {
            var builder = new MockSqlBuilder();
            builder.DeleteFrom("TEST");
            var sql = builder.Build();
            Assert.AreEqual("DELETE FROM TEST", sql.Query);
        }

        [TestMethod]
        public void TestUpdate()
        {
            var builder = new MockSqlBuilder();
            builder.Update("TEST");
            var sql = builder.Build();
            Assert.AreEqual("UPDATE TEST", sql.Query);
        }

        [TestMethod]
        public void TestGroupBy()
        {
            var builder = new MockSqlBuilder();
            builder.GroupBy("TEST1", "TEST2", "TEST3");
            var sql = builder.Build();
            Assert.AreEqual(" GROUP BY TEST1,TEST2,TEST3", sql.Query);
        }

        [TestMethod]
        public void TestOrderBy()
        {
            var builder = new MockSqlBuilder();
            builder.OrderBy("TEST", true);
            var sql = builder.Build();
            Assert.AreEqual(" ORDER BY TEST DESC", sql.Query);
        }

        [TestMethod]
        public void TestStartsWithColumn()
        {
            var builder = new MockSqlBuilder();
            builder.StartsWith("TEST", "VALUE");
            var sql = builder.Build();
            Assert.AreEqual(" TEST LIKE @0", sql.Query);
            Assert.AreEqual(1, sql.Parameters.Length);
            Assert.AreEqual("VALUE%", sql.Parameters[0]);
        }

        [TestMethod]
        public void TestEndsWithColumn()
        {
            var builder = new MockSqlBuilder();
            builder.EndsWith("TEST", "VALUE");
            var sql = builder.Build();
            Assert.AreEqual(" TEST LIKE @0", sql.Query);
            Assert.AreEqual(1, sql.Parameters.Length);
            Assert.AreEqual("%VALUE", sql.Parameters[0]);
        }

        [TestMethod]
        public void TestContainsColumn()
        {
            var builder = new MockSqlBuilder();
            builder.Contains("TEST", "VALUE");
            var sql = builder.Build();
            Assert.AreEqual(" TEST LIKE @0", sql.Query);
            Assert.AreEqual(1, sql.Parameters.Length);
            Assert.AreEqual("%VALUE%", sql.Parameters[0]);
        }

        [TestMethod]
        public void TestInColumn()
        {
            var builder = new MockSqlBuilder();
            builder.In("TEST", "VALUE1", "VALUE2", "VALUE3");
            var sql = builder.Build();
            Assert.AreEqual(" TEST IN (@0,@1,@2)", sql.Query);
            Assert.AreEqual(3, sql.Parameters.Length);
            Assert.AreEqual("VALUE1", sql.Parameters[0]);
            Assert.AreEqual("VALUE2", sql.Parameters[1]);
            Assert.AreEqual("VALUE3", sql.Parameters[2]);
        }

        [TestMethod]
        public void TestLeftJoin()
        {
            var builder = new MockSqlBuilder();
            builder.LeftJoin("TEST T2", "T2.COL2", "T1.COL1");
            var sql = builder.Build();
            Assert.AreEqual(" LEFT JOIN TEST T2 ON T2.COL2 = T1.COL1", sql.Query);
        }
    }
}
