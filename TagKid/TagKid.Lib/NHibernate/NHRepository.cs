using System;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Transform;
using Taga.Core.Repository;
using Taga.Core.Repository.Base;

namespace TagKid.Lib.NHibernate
{
    public class NHRepository : IRepository
    {
        private readonly ISession _session;

        public NHRepository()
        {
            var uow = (INHUnitOfWork)UnitOfWork.Current;
            _session = uow.Session;
        }

        public void Save<T>(T entity) where T : class
        {
            _session.SaveOrUpdate(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _session.Delete(entity);
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return _session.Query<T>();
        }

        public IList<T> Exec<T>(string spNameOrSql, IDictionary<string, object> args = null, bool rawSql = false) where T : class
        {
            var command = CreateSqlCommand(spNameOrSql, args, rawSql);

            var query = _session.CreateSQLQuery(command);
            query.AddEntity(typeof(T));

            if (args != null)
            {
                foreach (var arg in args)
                {
                    query.SetParameter(arg.Key, arg.Value);
                }
            }

            return query.List<T>();
        }

        private static string CreateSqlCommand(string spNameOrSql, IDictionary<string, object> args, bool rawSql)
        {
            if (rawSql)
            {
                return spNameOrSql;
            }

            // TODO: Here is mysql specific. Should be generalized!!!

            var sb = new StringBuilder();

            sb.Append("call ")
                .Append(spNameOrSql)
                .Append(" (");

            if (args != null)
            {
                var comma = String.Empty;

                foreach (var arg in args)
                {
                    sb.Append(comma)
                        .Append(":")
                        .Append(arg.Key);

                    comma = ",";
                }
            }

            return sb.Append(")")
                .ToString();
        }
    }
}
