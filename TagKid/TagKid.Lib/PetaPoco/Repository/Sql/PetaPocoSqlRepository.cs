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

        public override IPage<T> ExecuteQuery<T>(ISql sql, int pageIndex = 1, int pageSize = 1000)
        {
            var ppSql = (PetaPocoSql)sql;
            var petaPocoPage = _db.Page<T>(pageIndex, pageSize, ppSql.Sql);
            return new PetaPocoPage<T>(petaPocoPage);
        }

        public override int ExecuteNonQuery<T>(ISql sql)
        {
            var ppSql = (PetaPocoSql)sql;
            return _db.Execute(ppSql.Sql);
        }
    }
}
