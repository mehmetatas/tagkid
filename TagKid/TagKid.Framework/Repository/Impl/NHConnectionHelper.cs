using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace TagKid.Framework.Repository.Impl
{
    public static class NHConnectionHelper
    {
        public static ISessionFactory BuildSessionFactory<T>(string connectionStringName)
        {
            var fluentConfiguration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(c => c.FromConnectionStringWithKey(connectionStringName)))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<T>());

            fluentConfiguration.ExposeConfiguration(c => new SchemaUpdate(c).Execute(false, true));

            var fluentBuildUpConfiguration = fluentConfiguration.BuildConfiguration();
            
            SchemaMetadataUpdater.QuoteTableAndColumns(fluentBuildUpConfiguration); 
            
            return fluentBuildUpConfiguration.BuildSessionFactory(); 
        }
    }
}
