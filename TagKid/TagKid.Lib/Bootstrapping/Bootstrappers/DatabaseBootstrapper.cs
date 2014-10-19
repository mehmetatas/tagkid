using System.Configuration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Taga.Core.IoC;
using Taga.Core.Repository;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.NHibernate;

namespace TagKid.Lib.Bootstrapping.Bootstrappers
{
    public class DatabaseBootstrapper : IBootstrapper
    {
        public void Bootstrap(IServiceProvider prov)
        {
            prov.Register<ITransactionalUnitOfWork, NHUnitOfWork>();
            prov.Register<IUnitOfWork, NHUnitOfWork>();
            prov.Register<IRepository, NHRepository>();

            var sessionFactory = Fluently.Configure()
                .Database(
                    MySQLConfiguration.Standard
                        .ConnectionString(ConfigurationManager.ConnectionStrings["tagkid"].ConnectionString)
                        .ShowSql())
                .Mappings(mappingConfig => mappingConfig.FluentMappings.AddFromAssemblyOf<User>())
                .BuildSessionFactory();

            prov.Register(typeof (ISessionFactory), sessionFactory.GetType(), sessionFactory);
        }
    }
}