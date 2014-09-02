﻿using Taga.Core.Repository;
using Taga.Core.Repository.Base;

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