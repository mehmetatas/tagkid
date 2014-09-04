using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Taga.Core.Repository.Linq;

namespace TagKid.Tests.Core.Mock
{
    class MockLinqQueryBuilder<T> : ILinqQueryBuilder<T>
    {
        private readonly MockLinqQuery<T> _query = new MockLinqQuery<T>();

        public ILinqQueryBuilder<T> Where(Expression<Func<T, bool>> expression)
        {
            _query.WhereExpressions.Add(expression);
            return this;
        }

        public ILinqQueryBuilder<T> Page(int pageIndex, int pageSize)
        {
            return this;
        }

        public ILinqQueryBuilder<T> OrderBy(Expression<Func<T, dynamic>> expression, bool desc = false)
        {
            _query.OrderByExpressions.Add(new OrderByExpression<T, dynamic>
            {
                Expression = expression,
                Desc = desc
            });
            return this;
        }

        public ILinqQuery Build()
        {
            return _query;
        }
    }
}
