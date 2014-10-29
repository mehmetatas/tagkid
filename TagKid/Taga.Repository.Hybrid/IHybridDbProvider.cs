using System.Data;

namespace Taga.Repository.Hybrid
{
    public interface IHybridDbProvider
    {
        char ParameterPrefix { get; }

        string AppendSelectIdentity(string insertQuery);

        IDbConnection CreateConnection();
    }
}