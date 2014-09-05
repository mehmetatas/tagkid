namespace Taga.Core.Repository.Sql
{
    public interface ISqlBuilder
    {
        int ParamCount { get; }

        ISql Build();
        
        ISqlBuilder Append(string sql, params object[] parameters);

        ISqlBuilder SelectAllFrom(string tableName);
        ISqlBuilder Select(params string[] columns);
        ISqlBuilder From(string tableNamesAndAliases);

        ISqlBuilder Update(string tableName);
        ISqlBuilder DeleteFrom(string tableName);
        
        ISqlBuilder Column(string columnName);
        ISqlBuilder Param(object param);

        ISqlBuilder Where();
        ISqlBuilder Where(string columnName);
        ISqlBuilder And();
        ISqlBuilder And(string columnName);
        ISqlBuilder Or();
        ISqlBuilder Or(string columnName);

        ISqlBuilder Equals();
        ISqlBuilder EqualsParam(object param);
        ISqlBuilder Equals(string columnName, object param);

        ISqlBuilder NotEquals();
        ISqlBuilder NotEquals(object param);
        ISqlBuilder NotEquals(string columnName, object param);

        ISqlBuilder LessThan();
        ISqlBuilder LessThan(object param);
        ISqlBuilder LessThan(string columnName, object param);

        ISqlBuilder LessThanOrEquals();
        ISqlBuilder LessThanOrEquals(object param);
        ISqlBuilder LessThanOrEquals(string columnName, object param);

        ISqlBuilder GreaterThan();
        ISqlBuilder GreaterThan(object param);
        ISqlBuilder GreaterThan(string columnName, object param);

        ISqlBuilder GreaterThanOrEquals();
        ISqlBuilder GreaterThanOrEquals(object param);
        ISqlBuilder GreaterThanOrEquals(string columnName, object param);

        ISqlBuilder Between(object minParam, object maxParam);
        ISqlBuilder Between(string columnName, object minParam, object maxParam);
        
        ISqlBuilder GroupBy(params string[] columnNames);
        ISqlBuilder OrderBy(string columnName, bool desc);

        ISqlBuilder StartsWith(string value);
        ISqlBuilder StartsWith(string columnName, string value);
        ISqlBuilder EndsWith(string value);
        ISqlBuilder EndsWith(string columnName, string value);
        ISqlBuilder Contains(string value);
        ISqlBuilder Contains(string columnName, string value);

        ISqlBuilder In(params object[] parameters);
        ISqlBuilder In(string columnName, params object[] parameters);

        ISqlBuilder Join(string otherTable, string otherTableColumn, string thisTableColumn);
        ISqlBuilder LeftJoin(string otherTable, string otherTableColumn, string thisTableColumn);
    }
}