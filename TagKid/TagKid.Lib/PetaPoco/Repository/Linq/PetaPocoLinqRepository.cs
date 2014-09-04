using Taga.Core.Repository;
using Taga.Core.Repository.Base;
using Taga.Core.Repository.Linq;
using Taga.Core.Repository.Linq.Sql;

namespace TagKid.Lib.PetaPoco.Repository.Linq
{
    public class PetaPocoLinqRepository : ILinqRepository
    {
        private readonly Database _db;

        public PetaPocoLinqRepository()
        {
            _db = ((PetaPocoUnitOfWork)UnitOfWorkBase.Current).Db;
        }

        public void Save<T>(T entity)
        {
            _db.Save(entity);
        }

        public void Delete<T>(T entity)
        {
            _db.Delete(entity);
        }

        public IPage<T> Query<T>(ILinqQueryBuilder<T> queryBuilder)
        {
            var sql = (ILinqSqlQuery)queryBuilder.Build();

            var ppPage = _db.Page<T>(
                sql.PageIndex,
                sql.PageSize,
                sql.Query,
                sql.Parameters);

            return new PetaPocoPage<T>(ppPage);
        }
    }
}
