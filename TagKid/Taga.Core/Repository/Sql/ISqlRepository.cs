using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using Taga.Core.IoC;

namespace Taga.Core.Repository.Sql
{
    public interface ISqlRepository
    {
        void Save<T>(T entity);
        void Delete<T>(T entity);
        IPage<T> ExecuteQuery<T>(ISql sql, int pageIndex = 1, int pageSize = 1000);
        int ExecuteNonQuery<T>(ISql sql);
    }

    public static class SqlRepositoryExtensions
    {
        public static IEnumerable<T> List<T>(this ISqlRepository repo, ISql sql)
        {
            return repo.ExecuteQuery<T>(sql).Items;
        }

        public static T FirstOrDefault<T>(this ISqlRepository repo, ISql sql)
        {
            return repo.List<T>(sql).FirstOrDefault();
        }

        public static T FirstOrDefault<T>(this ISqlRepository repo, Expression<Func<T, dynamic>> propExpression, object value) where T : class, new()
        {
            var builder = ServiceProvider.Provider.GetOrCreate<ISqlBuilder>();

            builder.Select(propExpression, value);

            return repo.FirstOrDefault<T>(builder.Build());
        }
    }
}
