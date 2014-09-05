using Taga.Core.Repository.Sql;

namespace TagKid.Lib.PetaPoco.Repository.Sql
{
    class PetaPocoSql : ISql
    {
        private readonly PetaPoco.Sql _sql;

        public PetaPocoSql(string sql, params object[] parameters)
        {
            _sql = new PetaPoco.Sql(sql, parameters);
        }

        internal PetaPoco.Sql Sql
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
