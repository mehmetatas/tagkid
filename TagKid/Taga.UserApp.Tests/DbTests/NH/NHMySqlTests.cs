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
    public class NHMySqlTests : UserAppDbTests
    {
        protected override void InitDb()
        {
            var sessionFactory = Fluently.Configure()
                .Database(MySQLConfiguration.Standard
                    .ConnectionString(ConfigurationManager.ConnectionStrings["user_app_mysql"].ConnectionString)
                    .ShowSql())
                .Mappings(mappingConfig => mappingConfig.FluentMappings.AddFromAssemblyOf<User>())
                .BuildSessionFactory();

            var prov = ServiceProvider.Provider;

            prov.RegisterSingleton<INHSpCallBuilder>(new MySqlSpCallBuilder());
            prov.Register<IUnitOfWork, NHUnitOfWork>();
            prov.Register<ITransactionalUnitOfWork, NHUnitOfWork>();
            prov.Register<IRepository, NHRepository>();
            prov.RegisterSingleton(sessionFactory);
        }

        protected override bool IsSqlServer
        {
            get { return false; }
        }

        protected override bool IsEntityFramework
        {
            get { return false; }
        }
    }
}
