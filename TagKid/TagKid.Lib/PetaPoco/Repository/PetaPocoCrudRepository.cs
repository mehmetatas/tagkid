using System;
using Taga.Core.Repository;
using Taga.Core.Repository.Base;
using Taga.Core.Repository.Linq;

namespace TagKid.Lib.PetaPoco.Repository
{
    class PetaPocoCrudRepository : ICrudRepository
    {
        private readonly Database _db;

        public PetaPocoCrudRepository() {
            _db = ((PetaPocoUnitOfWork)UnitOfWork.Current).Db;
        }

        public void Save<T>(T entity)
        {
            _db.Save(entity);
        }

        public void Delete<T>(T entity) {
            _db.Delete(entity);
        }

        public IPage<T> Select<T>(ILinqQuery<T> query)
        {
            var builder = (ILinqQueryBuilder<Taga.Core.Repository.Linq.Sql>)query;
            var sql = builder.Build();
            
            var ppPage = _db.Page<T>(
                sql.PageIndex, 
                sql.PageSize, 
                String.Format("WHERE {0} ORDER BY {1}", sql.Where, sql.OrderBy), 
                sql.Parameters);

            return new PetaPocoPage<T>(ppPage);
        }
    }
}
