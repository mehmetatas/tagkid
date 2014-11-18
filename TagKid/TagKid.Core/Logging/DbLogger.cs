using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Taga.Core.Logging;

namespace TagKid.Core.Logging
{
    public class DbLogger : ILogger
    {
        private IDbCommand _cmd;
        private IDbConnection _conn;

        public void Log(ILog log)
        {
            EnsureCommand();

            AddParam("d", log.Date);
            AddParam("l", log.Level);
            AddParam("e", log.ErrorCode);
            AddParam("m", log.Message);
            AddParam("u", log.User);
            AddParam("de", log.Details);

            _cmd.ExecuteNonQuery();
        }

        private void EnsureCommand()
        {
            if (_cmd == null || _conn == null || _conn.State != ConnectionState.Open)
            {
                EnsureConnection();
                CreateCommand();
            }

            _cmd.Parameters.Clear();
        }

        private void EnsureConnection()
        {
            if (_conn != null)
            {
                try
                {
                    if (_conn.State != ConnectionState.Closed)
                    {
                        _conn.Close();   
                    }
                    _conn.Dispose();
                }
                catch (Exception exception)
                {
                    L.E("Unable to close DbLogger connection", exception);
                }
            }

            try
            {
                _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["tagkid"].ConnectionString);
                _conn.Open();
            }
            catch (Exception exception)
            {
                L.E("Unable to open DbLogger connection", exception);
            }
        }

        private void CreateCommand()
        {
            _cmd = _conn.CreateCommand();
            _cmd.CommandText = "INSERT INTO Log (Date, [Level], ErrorCode, Message, [User], Details) VALUES (@d, @l, @e, @m, @u, @de)";
        }

        private void AddParam(string name, object value)
        {
            var param = _cmd.CreateParameter();
            param.ParameterName = name;
            param.Value = value ?? DBNull.Value;
            _cmd.Parameters.Add(param);
        }
    }
}
