using System.Collections.Generic;
using Taga.Core.DynamicProxy;
using Taga.Core.Repository;
using BaseRepository = Taga.Core.Repository.Base.Repository;

namespace TagKid.Lib.PetaPoco.Repository
{
    [Intercept]
    public class PetaPocoRepository : BaseRepository
    {
        private readonly Database _db;

        public PetaPocoRepository()
        {
            _db = ((PetaPocoUnitOfWork)UnitOfWork).Db;
        }

        public override void Save<T>(T entity)
        {
            _db.Save(entity);
        }

        public override T Get<T>(ISql sql)
        {
            var ppSql = (PetaPocoSql)sql;
            return _db.SingleOrDefault<T>(ppSql.Sql);
        }

        public override T Scalar<T>(ISql sql)
        {
            var ppSql = (PetaPocoSql)sql;
            return _db.ExecuteScalar<T>(ppSql.Sql);
        }

        public override List<T> Select<T>(ISql sql)
        {
            var ppSql = (PetaPocoSql)sql;
            return _db.Fetch<T>(ppSql.Sql);
        }

        public override IPage<T> Page<T>(int page, int pageSize, ISql sql)
        {
            var ppSql = (PetaPocoSql)sql;
            var petaPocoPage = _db.Page<T>(page, pageSize, ppSql.Sql);
            return new PetaPocoPage<T>(petaPocoPage);
        }
    }
}
