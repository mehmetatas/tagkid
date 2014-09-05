using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Taga.Core.Repository.Linq;

namespace TagKid.Tests.Core.Mock
{
    class MockLinqQuery<TEntity> : ILinqQuery
    {
        public MockLinqQuery()
        {
            WhereExpressions = new List<Expression<Func<TEntity, bool>>>();
            OrderByExpressions = new List<OrderByExpression<TEntity, dynamic>>();
        }

        public List<Expression<Func<TEntity, bool>>> WhereExpressions { get; private set; }
        public List<OrderByExpression<TEntity, dynamic>> OrderByExpressions { get; private set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    class OrderByExpression<TEntity, TProp>
    {
        public Expression<Func<TEntity, TProp>> Expression { get; set; }
        public bool Desc { get; set; }
    }
}
