using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Taga.Core.Repository.Hybrid;

namespace TagKid.Application.Bootstrapping.Bootstrappers
{
    public class TagKidHybridDbProvider : IHybridDbProvider
    {
        public char ParameterPrefix
        {
            get { return '@'; }
        }

        public object Insert(Type type, IDbCommand cmd, bool selectId)
        {
            if (selectId)
            {
                cmd.CommandText += "; SELECT SCOPE_IDENTITY();";
            }
            return cmd.ExecuteScalar();
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["tagkid"].ConnectionString);
        }
    }
}