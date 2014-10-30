using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using IQueryProvider = Taga.Core.Repository.IQueryProvider;

namespace Taga.Repository.NH
{
    public class NHQueryProvider : IQueryProvider
    {
        private ISession _session;
        private readonly ISessionFactory _sessionFactory;
        private readonly INHSpCallBuilder _spCallBuilder;

        public NHQueryProvider(ISessionFactory sessionFactory, INHSpCallBuilder spCallBuilder)
        {
            _sessionFactory = sessionFactory;
            _spCallBuilder = spCallBuilder;
        }

        public void SetConnection(IDbConnection connection)
        {
            _session = _sessionFactory.OpenSession(connection);
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return _session.Query<T>();
        }

        public IList<T> Exec<T>(string spNameOrSql, IDictionary<string, object> args = null, bool rawSql = false) where T : class
        {
            return NHRepository.Exec<T>(_session, _spCallBuilder, spNameOrSql, args, rawSql);
        }
    }
}
