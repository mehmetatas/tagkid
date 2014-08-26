using System;
using System.Collections.Generic;
using Taga.Core.Repository;
using TagKid.Lib.PetaPoco;

namespace TagKid.Lib.Repository
{
    public class Db : IRepository
    {
        private readonly IRepository _repository;

        public Db(bool useTransaction)
        {
            _repository = PetaPocoRepository.New(useTransaction);
        }

        public void Save<T>(T entity) where T : class, new()
        {
            _repository.Save(entity);
        }

        public T Get<T>(ISql sql)
        {
            return _repository.Get<T>(sql);
        }

        public T Scalar<T>(ISql sql)
        {
            return _repository.Scalar<T>(sql);
        }

        public List<T> Select<T>(ISql sql) where T : class, new()
        {
            return _repository.Select<T>(sql);
        }

        public IPage<T> Page<T>(int page, int pageSize, ISql sql)
        {
            return _repository.Page<T>(page, pageSize, sql);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public static ISql Sql()
        {
            return Sql(String.Empty);
        }

        public static ISql Sql(string sql, params  object[] parameters)
        {
            return new PetaPocoSqlAdapter().Append(sql, parameters);
        }
    }
}
