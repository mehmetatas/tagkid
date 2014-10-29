using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using Taga.Core.IoC;
using Taga.Core.Repository;
using Taga.Repository.NH;
using Taga.Repository.NH.SpCallBuilders;
using Taga.UserApp.Core.Model.Database;

namespace Taga.UserApp.Tests.DbTests.NH
{
    [TestClass]
    public class NHSqlServerTests : UserAppDbTests
    {
        protected override void InitDb()
        {
            var sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString(ConfigurationManager.ConnectionStrings["user_app_sqlserver_nh"].ConnectionString)
                    .ShowSql())
                .Mappings(mappingConfig => mappingConfig.FluentMappings.AddFromAssemblyOf<User>())
                .BuildSessionFactory();

            var prov = ServiceProvider.Provider;

            prov.Register<INHSpCallBuilder, SqlServerSpCallBuilder>();
            prov.Register<IUnitOfWork, NHUnitOfWork>();
            prov.Register<ITransactionalUnitOfWork, NHUnitOfWork>();
            prov.Register<IRepository, NHRepository>();
            prov.RegisterSingleton(sessionFactory);
        }
    }
}
