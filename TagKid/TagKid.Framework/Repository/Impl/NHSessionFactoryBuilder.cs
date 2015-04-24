using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace TagKid.Framework.Repository.Impl
{
    public static class NHSessionFactoryBuilder
    {
        public static ISessionFactory Build<T>(string connStrName)
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(c => c.FromConnectionStringWithKey(connStrName)))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<T>())
                .ExposeConfiguration(CreateSchema)
                .BuildSessionFactory();
        }

        private static void CreateSchema(Configuration config)
        {
            SchemaMetadataUpdater.QuoteTableAndColumns(config);
            var update = new SchemaUpdate(config);
            update.Execute(false, true);
        }
    }
}
