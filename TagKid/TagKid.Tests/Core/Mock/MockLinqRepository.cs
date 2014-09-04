using System;
using System.Collections.Generic;
using System.Linq;
using Taga.Core.Repository;
using Taga.Core.Repository.Linq;

namespace TagKid.Tests.Core.Mock
{
    class MockLinqRepository : ILinqRepository
    {
        private readonly Dictionary<Type, List<object>> _tables = new Dictionary<Type, List<object>>();

        public void Save<T>(T entity)
        {
            GetTable<T>().Add(entity);
        }

        public void Delete<T>(T entity)
        {
            GetTable<T>().Remove(entity);
        }

        public IPage<T> Query<T>(ILinqQueryBuilder<T> queryBuilder)
        {
            var linqQuery = (MockLinqQuery<T>)queryBuilder.Build();

            var table = (IEnumerable<T>)GetTable<T>();

            foreach (var whereExpression in linqQuery.WhereExpressions)
            {
                table = table.Where(whereExpression.Compile());
            }

            var count = table.Count();

            if (linqQuery.PageSize > 0 && linqQuery.PageIndex > -1)
                table = table.Skip(linqQuery.PageSize * linqQuery.PageIndex).Take(linqQuery.PageSize);

            IOrderedEnumerable<T> orderedTable = null;

            foreach (var orderByExpression in linqQuery.OrderByExpressions)
            {
                if (orderedTable == null)
                {
                    if (orderByExpression.Desc)
                        orderedTable = table.OrderBy(orderByExpression.Expression.Compile());
                    else
                        orderedTable = table.OrderByDescending(orderByExpression.Expression.Compile());
                }
                else
                {
                    if (orderByExpression.Desc)
                        orderedTable = orderedTable.ThenBy(orderByExpression.Expression.Compile());
                    else
                        orderedTable = orderedTable.ThenByDescending(orderByExpression.Expression.Compile());
                }
            }

            return new MockPage<T>
            {
                CurrentPage = linqQuery.PageIndex,
                PageSize = linqQuery.PageSize,
                Items = table.ToList(),
                TotalCount = count,
                TotalPages = ((count - 1) / linqQuery.PageSize) + 1
            };
        }

        private List<object> GetTable<T>()
        {
            if (!_tables.ContainsKey(typeof(T)))
                _tables.Add(typeof(T), new List<object>());
            return _tables[typeof(T)];
        }
    }
}
