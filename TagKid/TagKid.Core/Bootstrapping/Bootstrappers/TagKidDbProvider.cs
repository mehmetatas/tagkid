using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DummyOrm.Providers.SqlServer2012;

namespace TagKid.Core.Bootstrapping.Bootstrappers
{
    class TagKidDbProvider : SqlServer2012Provider
    {
        public override IDbConnection CreateConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["tagkid"].ConnectionString);
        }
    }
}