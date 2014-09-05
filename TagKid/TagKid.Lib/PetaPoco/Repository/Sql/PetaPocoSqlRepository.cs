using System.Collections.Generic;
using Taga.Core.DynamicProxy;
using Taga.Core.Repository;
using Taga.Core.Repository.Sql;
using Taga.Core.Repository.Sql.Base;

namespace TagKid.Lib.PetaPoco.Repository.Sql
{
    [Intercept]
    public class PetaPocoSqlRepository : SqlRepositoryBase
    {
        private readonly Database _db;

        public PetaPocoSqlRepository()
        {
            _db = ((PetaPocoUnitOfWork)UnitOfWork).Db;
        }

        public override void Save<T>(T entity)
        {
            _db.Save(entity);
        }

        public override void Delete<T>(T entity)
        {
            _db.Delete(entity);
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

        public override IPage<T> Page<T>(int pageIndex, int pageSize, ISql sql)
        {
            var ppSql = (PetaPocoSql)sql;
            var petaPocoPage = _db.Page<T>(pageIndex, pageSize, ppSql.Sql);
            return new PetaPocoPage<T>(petaPocoPage);
        }
    }
}
