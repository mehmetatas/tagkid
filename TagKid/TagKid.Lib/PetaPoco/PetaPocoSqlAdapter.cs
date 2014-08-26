using Taga.Core.Repository;

namespace TagKid.Lib.PetaPoco
{
    class PetaPocoSqlAdapter : ISql
    {
        private readonly Sql _sql;

        internal Sql Sql
        {
            get { return _sql; }
        }

        internal PetaPocoSqlAdapter()
        {
            _sql = new Sql();
        }

        public ISql Append(string sql, params object[] parameters)
        {
            _sql.Append(sql, parameters);
            return this;
        }
    }
}
