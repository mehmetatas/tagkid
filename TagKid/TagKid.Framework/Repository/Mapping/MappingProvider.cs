
using System;

namespace TagKid.Framework.Repository.Mapping
{
    public class MappingProvider
    {
        public static readonly MappingProvider Instance = new MappingProvider();

        private DatabaseMapping _databaseMapping;

        private MappingProvider()
        {
        }

        public void SetDatabaseMapping(DatabaseMapping databaseMapping)
        {
            _databaseMapping = databaseMapping;
        }

        public DatabaseMapping GetDatabaseMapping()
        {
            return _databaseMapping;
        }
    }

    public static class MappingProviderExtensions
    {
        public static TableMapping GetTableMapping<T>(this MappingProvider prov) where T : class
        {
            return prov.GetTableMapping(typeof(T));
        }

        public static TableMapping GetTableMapping(this MappingProvider prov, Type entityType)
        {
            return prov.GetDatabaseMapping()[entityType];
        }
    }
}