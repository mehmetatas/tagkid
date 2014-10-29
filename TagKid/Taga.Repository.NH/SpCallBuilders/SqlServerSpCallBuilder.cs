using System;
using System.Collections.Generic;
using System.Text;

namespace Taga.Repository.NH.SpCallBuilders
{
    public class SqlServerSpCallBuilder : INHSpCallBuilder
    {
        public string BuildSpCall(string spNameOrSql, IDictionary<string, object> args)
        {
            var sb = new StringBuilder();

            sb.Append("exec ")
                .Append(spNameOrSql);

            if (args != null)
            {
                sb.Append(" (");

                var comma = String.Empty;

                foreach (var arg in args)
                {
                    sb.Append(comma)
                        .Append(":")
                        .Append(arg.Key);

                    comma = ",";
                }

                sb.Append(")");
            }

            return sb.ToString();
        }
    }
}
