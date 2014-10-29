using System.Collections.Generic;

namespace Taga.Repository.NH
{
    public interface INHSpCallBuilder
    {
        string BuildSpCall(string spNameOrSql, IDictionary<string, object> args);
    }
}
