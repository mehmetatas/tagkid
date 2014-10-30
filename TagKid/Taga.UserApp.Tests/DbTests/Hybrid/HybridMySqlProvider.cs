using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using Taga.Repository.Hybrid;

namespace Taga.UserApp.Tests.DbTests.Hybrid
{
    public class HybridMySqlProvider : IHybridDbProvider
    {
        public char ParameterPrefix
        {
            get { return '?'; }
        }

        public string AppendSelectIdentity(string insertQuery)
        {
            return insertQuery + "; SELECT LAST_INSERT_ID();";
        }

        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(ConfigurationManager.ConnectionStrings["user_app_mysql"].ConnectionString);
        }
    }
}
