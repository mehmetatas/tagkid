using Taga.Core.Repository;
using Taga.Core.Repository.Base;

namespace TagKid.Lib.PetaPoco.Repository
{
    public class PetaPocoSqlBuilder : SqlBuilder
    {
        protected override ISql BuildSql(string sql, object[] parameters)
        {
            return new PetaPocoSql(sql, parameters);
        }
    }
}
