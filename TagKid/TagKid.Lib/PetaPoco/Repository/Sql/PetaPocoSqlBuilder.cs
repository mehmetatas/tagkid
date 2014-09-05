using Taga.Core.Repository.Sql;
using Taga.Core.Repository.Sql.Base;

namespace TagKid.Lib.PetaPoco.Repository.Sql
{
    public class PetaPocoSqlBuilder : SqlBuilder
    {
        protected override ISql BuildSql(string sql, object[] parameters)
        {
            return new PetaPocoSql(sql, parameters);
        }
    }
}
