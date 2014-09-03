namespace Taga.Core.Repository
{
    public interface ISqlBuilder
    {
        ISql Build();
        
        ISqlBuilder Append(string sql, params object[] parameters);

        ISqlBuilder SelectAllFrom(string tableName);
        ISqlBuilder Select(params string[] columns);
        ISqlBuilder From(string tableNamesAndAliases);

        ISqlBuilder Update(string tableName);
        ISqlBuilder DeleteFrom(string tableName);
        
        ISqlBuilder Column(string columnName);
        ISqlBuilder Param(object param);

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
        ISqlBuilder OrderBy(string columnName);
        ISqlBuilder Desc();
    }
}