using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Taga.Core.Repository.Linq;

namespace TagKid.Tests.Core.Mock
{
    class MockLinqQuery<TEntity> : ILinqQuery
    {
        public List<Expression<Func<TEntity, bool>>> WhereExpressions = new List<Expression<Func<TEntity, bool>>>();
        public List<OrderByExpression<TEntity, dynamic>> OrderByExpressions = new List<OrderByExpression<TEntity, dynamic>>();
        public int PageIndex;
        public int PageSize;
    }

    class OrderByExpression<TEntity, TProp>
    {
        public Expression<Func<TEntity, TProp>> Expression { get; set; }
        public bool Desc { get; set; }
    }
}
