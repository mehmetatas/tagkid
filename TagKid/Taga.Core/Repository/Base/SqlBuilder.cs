using System;
using System.Collections.Generic;
using System.Text;

namespace Taga.Core.Repository.Base
{
    public abstract class SqlBuilder : ISqlBuilder
    {
        private readonly List<object> _parameters;
        private readonly StringBuilder _query;

        protected SqlBuilder()
        {
            _query = new StringBuilder();
            _parameters = new List<object>();
        }

        public ISql Build()
        {
            return BuildSql(_query.ToString(), _parameters.ToArray());
        }

        protected abstract ISql BuildSql(string sql, object[] parameters);

        public ISqlBuilder Append(string sql, params object[] parameters)
        {
            _query.Append(sql);
            _parameters.AddRange(parameters);
            return this;
        }

        public ISqlBuilder SelectAllFrom(string tableName)
        {
            return Append(String.Format("SELECT * FROM {0}", tableName));
        }

        public ISqlBuilder Select(params string[] columns)
        {
            return Append(String.Format("SELECT {0}", String.Join(",", columns)));
        }

        public ISqlBuilder From(string tableNamesAndAliases)
        {
            return Append(String.Format(" FROM {0}", tableNamesAndAliases));
        }

        public ISqlBuilder Update(string tableName)
        {
            return Append(String.Format("UPDATE {0}", tableName));
        }

        public ISqlBuilder DeleteFrom(string tableName)
        {
            return Append(String.Format("DELETE FROM {0}", tableName));
        }

        public ISqlBuilder Where(string columnName)
        {
            return Append(String.Format(" WHERE {0}", columnName));
        }

        public ISqlBuilder Column(string columnName)
        {
            return Append(String.Format(" {0}", columnName));
        }

        public ISqlBuilder Param(object param)
        {
            return Append(String.Format(" @{0}", _parameters.Count), param);
        }

        public ISqlBuilder And()
        {
            return Append(" AND");
        }

        public ISqlBuilder And(string columnName)
        {
            return And()
                .Column(columnName);
        }

        public ISqlBuilder Or()
        {
            return Append(" OR");
        }

        public ISqlBuilder Or(string columnName)
        {
            return Or()
                .Column(columnName);
        }

        public ISqlBuilder Equals()
        {
            return Append(" =");
        }

        public ISqlBuilder EqualsParam(object param)
        {
            return Equals()
                .Param(param);
        }

        public ISqlBuilder Equals(string columnName, object param)
        {
            return
                Column(columnName)
                .Equals()
                .Param(param);
        }

        public ISqlBuilder NotEquals()
        {
            return Append(" <>");
        }

        public ISqlBuilder NotEquals(object param)
        {
            return NotEquals()
                .Param(param);
        }

        public ISqlBuilder NotEquals(string columnName, object param)
        {
            return Column(columnName)
                .NotEquals(param);
        }

        public ISqlBuilder LessThan()
        {
            return Append(" <");
        }

        public ISqlBuilder LessThan(object param)
        {
            return LessThan()
                .Param(param);
        }

        public ISqlBuilder LessThan(string columnName, object param)
        {
            return Column(columnName)
                .LessThan(param);
        }

        public ISqlBuilder LessThanOrEquals()
        {
            return Append(" <=");
        }

        public ISqlBuilder LessThanOrEquals(object param)
        {
            return LessThanOrEquals()
                .Param(param);
        }

        public ISqlBuilder LessThanOrEquals(string columnName, object param)
        {
            return Column(columnName)
                .LessThanOrEquals(param);
        }

        public ISqlBuilder GreaterThan()
        {
            return Append(" >");
        }

        public ISqlBuilder GreaterThan(object param)
        {
            return GreaterThan()
                .Param(param);
        }

        public ISqlBuilder GreaterThan(string columnName, object param)
        {
            return Column(columnName)
                  .GreaterThan(param);
        }

        public ISqlBuilder GreaterThanOrEquals()
        {
            return Append(" >=");
        }

        public ISqlBuilder GreaterThanOrEquals(object param)
        {
            return GreaterThanOrEquals()
                .Param(param);
        }

        public ISqlBuilder GreaterThanOrEquals(string columnName, object param)
        {
            return Column(columnName)
                  .GreaterThanOrEquals(param);
        }

        public ISqlBuilder Between(object minParam, object maxParam)
        {
            return Append(" BETWEEN")
                .Param(minParam)
                .And()
                .Param(maxParam);
        }

        public ISqlBuilder Between(string columnName, object minParam, object maxParam)
        {
            return Column(columnName)
                  .Between(minParam, maxParam);
        }
    }
}
