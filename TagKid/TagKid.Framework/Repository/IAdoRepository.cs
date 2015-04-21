using System.Collections.Generic;
using System.Data;

namespace TagKid.Framework.Repository
{
    public interface IAdoRepository
    {
        object ExecuteScalar(string commandText, IDictionary<string, object> commandParameters = null, CommandType commandType = CommandType.Text);

        int ExecuteNonQuery(string commandText, IDictionary<string, object> commandParameters = null, CommandType commandType = CommandType.Text);

        IDataReader ExecuteReader(string commandText, IDictionary<string, object> commandParameters = null, CommandType commandType = CommandType.Text);
    }
}
