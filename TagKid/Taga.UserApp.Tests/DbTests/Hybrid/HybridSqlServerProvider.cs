using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Taga.Repository.Hybrid;

namespace Taga.UserApp.Tests.DbTests.Hybrid
{
    public class HybridSqlServerProvider : IHybridDbProvider
    {
        public char ParameterPrefix
        {
            get { return '@'; }
        }

        public string AppendSelectIdentity(string insertQuery)
        {
            return insertQuery + "; SELECT SCOPE_IDENTITY();";
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["user_app_sqlserver"].ConnectionString);
        }
    }
}
