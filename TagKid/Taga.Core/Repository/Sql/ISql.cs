namespace Taga.Core.Repository.Sql
{
    public interface ISql
    {
        string Query { get; }
        object[] Parameters { get; }
    }
}
