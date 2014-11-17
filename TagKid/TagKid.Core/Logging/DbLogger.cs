using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Taga.Core.Logging;

namespace TagKid.Core.Logging
{
    public class DbLogger : ILogger
    {
        private readonly IDbCommand _cmd;

        public DbLogger()
        {
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["tagkid"].ConnectionString);
            conn.Open();
            _cmd = conn.CreateCommand();
            _cmd.CommandText = "INSERT INTO Log (Date, [Level], ErrorCode, Message, [User], Details) VALUES (@d, @l, @e, @m, @u, @de)";
        }

        public void Log(ILog log)
        {
            _cmd.Parameters.Clear();

            var param = _cmd.CreateParameter();
            param.ParameterName = "d";
            param.Value = log.Date;

            param = _cmd.CreateParameter();
            param.ParameterName = "l";
            param.Value = (int)log.Level;

            param = _cmd.CreateParameter();
            param.ParameterName = "e";
            param.Value = log.ErrorCode;

            param = _cmd.CreateParameter();
            param.ParameterName = "m";
            param.Value = log.Message;

            param = _cmd.CreateParameter();
            param.ParameterName = "u";
            param.Value = log.User;

            param = _cmd.CreateParameter();
            param.ParameterName = "de";
            param.Value = log.Details;

            _cmd.ExecuteNonQuery();
        }
    }
}
