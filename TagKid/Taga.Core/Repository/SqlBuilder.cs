using System;

namespace Taga.Core.Repository
{
    public class SqlBuilder
    {
        private int _paramIndex;
        private readonly ISql _sql;

        public SqlBuilder(ISql sql)
        {
            _sql = sql;
        }

        public ISql Build()
        {
            return _sql;
        }

        public SqlBuilder Append(string sql, params object[] parameters)
        {
            _sql.Append(sql, parameters);
            return this;
        }

        public SqlBuilder Select(params string[] columns)
        {
            Append(String.Format("SELECT {0}", String.Join(",", columns)));
            return this;
        }

        public SqlBuilder From(string tableNamesAndAliases)
        {
            Append(String.Format(" FROM {0}", tableNamesAndAliases));
            return this;
        }

        public SqlBuilder Where(string columnName)
        {
            Append(String.Format(" WHERE {0}", columnName));
            return this;
        }

        public SqlBuilder Param(object param)
        {
            Append(String.Format(" @{0}", _paramIndex), param);
            return this;
        }

        public SqlBuilder And()
        {
            Append(" AND");
            return this;
        }

        public SqlBuilder Or()
        {
            Append(" OR");
            return this;
        }

        public SqlBuilder Equals()
        {
            Append(" =");
            return this;
        }

        public SqlBuilder NotEquals()
        {
            Append(" <>");
            return this;
        }

        public SqlBuilder LessThan()
        {
            Append(" <");
            return this;
        }

        public SqlBuilder LessThanOrEquals()
        {
            Append(" <=");
            return this;
        }

        public SqlBuilder GreaterThan()
        {
            Append(" >");
            return this;
        }

        public SqlBuilder GreaterThanOrEquals()
        {
            Append(" >=");
            return this;
        }

        public SqlBuilder Between(object min, object max)
        {
            Append(String.Format(" BETWEEN @{0} AND @{1}", _paramIndex, _paramIndex + 1), min, max);
            _paramIndex += 2;
            return this;
        }
    }
}
