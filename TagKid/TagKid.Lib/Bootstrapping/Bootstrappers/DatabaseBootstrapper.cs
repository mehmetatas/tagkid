using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System.Configuration;
using NHibernate.SqlCommand;
using Taga.Core.IoC;
using Taga.Core.Repository;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.NHibernate;
using IServiceProvider = Taga.Core.IoC.IServiceProvider;

namespace TagKid.Lib.Bootstrapping.Bootstrappers
{
    public class DatabaseBootstrapper : IBootstrapper
    {
        public void Bootstrap(IServiceProvider prov)
        {
            prov.Register<IUnitOfWork, NHUnitOfWork>();
            prov.Register<IRepository, NHRepository>();

            var sessionFactory = Fluently.Configure()
                .Database(
                    MySQLConfiguration.Standard
                    .ConnectionString(ConfigurationManager.ConnectionStrings["tagkid"].ConnectionString)
                    .ShowSql())
                .Mappings(mappingConfig => mappingConfig.FluentMappings.AddFromAssemblyOf<User>())
                .ExposeConfiguration(cfg => cfg.SetInterceptor(new SqlStatementInterceptor()))
                .BuildSessionFactory();

            prov.Register(typeof(ISessionFactory), sessionFactory.GetType(), sessionFactory);
        }
    }

    public class SqlStatementInterceptor : EmptyInterceptor
    {
        public override SqlString OnPrepareStatement(SqlString sql)
        {
            // Console.WriteLine(sql);
            return base.OnPrepareStatement(sql);
        }
    }
}
