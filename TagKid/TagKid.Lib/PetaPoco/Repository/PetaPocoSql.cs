using Taga.Core.Repository;

namespace TagKid.Lib.PetaPoco.Repository
{
    class PetaPocoSql : ISql
    {
        private readonly Sql _sql;

        public PetaPocoSql(string sql, params object[] parameters)
        {
            _sql = new Sql(sql, parameters);
        }

        public Sql Sql
        {
            get { return _sql; }
        }

        public string Query
        {
            get { return _sql.SQL; }
        }

        public object[] Parameters
        {
            get { return _sql.Arguments; }
        }
    }
}
