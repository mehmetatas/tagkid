using System.Collections.Generic;
using System.Data;

namespace TagKid.Framework.Repository.NH
{
    public class NHAdoRepository : IAdoRepository
    {
        public object ExecuteScalar(string commandText, IDictionary<string, object> commandParameters = null, CommandType commandType = CommandType.Text)
        {
            var cmd = PrepareCommand(commandText, commandParameters, commandType, true);

            return cmd.ExecuteScalar();
        }

        public int ExecuteNonQuery(string commandText, IDictionary<string, object> commandParameters = null, CommandType commandType = CommandType.Text)
        {
            var cmd = PrepareCommand(commandText, commandParameters, commandType, true);

            return cmd.ExecuteNonQuery();
        }

        public IDataReader ExecuteReader(string commandText, IDictionary<string, object> commandParameters = null, CommandType commandType = CommandType.Text)
        {
            var cmd = PrepareCommand(commandText, commandParameters, commandType, false);

            return cmd.ExecuteReader();
        }

        private static IDbCommand PrepareCommand(string commandText, IDictionary<string, object> commandParameters, CommandType commandType, bool openTransaction)
        {
            var cmd = NHUnitOfWork.Current.GetSession(openTransaction).CreateCommand();

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
