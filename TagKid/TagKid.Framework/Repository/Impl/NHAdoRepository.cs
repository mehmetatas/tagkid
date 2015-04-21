using System.Collections.Generic;
using System.Data;

namespace TagKid.Framework.Repository.Impl
{
    public class NHAdoRepository : IAdoRepository
    {
        private static INHSession Session
        {
            get { return NHUnitOfWork.Current.GetSession(true); }
        }

        public object ExecuteScalar(string commandText, IDictionary<string, object> commandParameters = null, CommandType commandType = CommandType.Text)
        {
            var cmd = PrepareCommand(commandText, commandParameters, commandType);

            return cmd.ExecuteScalar();
        }

        public int ExecuteNonQuery(string commandText, IDictionary<string, object> commandParameters = null, CommandType commandType = CommandType.Text)
        {
            var cmd = PrepareCommand(commandText, commandParameters, commandType);

            return cmd.ExecuteNonQuery();
        }

        public IDataReader ExecuteReader(string commandText, IDictionary<string, object> commandParameters = null, CommandType commandType = CommandType.Text)
        {
            var cmd = PrepareCommand(commandText, commandParameters, commandType);

            return cmd.ExecuteReader();
        }

        private static IDbCommand PrepareCommand(string commandText, IDictionary<string, object> commandParameters, CommandType commandType)
        {
            var cmd = Session.CreateCommand();

            cmd.CommandText = commandText;
            cmd.CommandType = commandType;

            if (commandParameters != null)
            {
                foreach (var cmdParam in commandParameters)
                {
                    var param = cmd.CreateParameter();
                    param.ParameterName = cmdParam.Key;
                    param.Value = cmdParam.Value;
                    cmd.Parameters.Add(param);
                }
            }
            return cmd;
        }
    }
}
