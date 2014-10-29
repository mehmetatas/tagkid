using System;

namespace Taga.Core.Repository.Mapping
{
    public interface IMappingProvider
    {
        void SetDatabaseMapping(DatabaseMapping mappings);

        DatabaseMapping GetDatabaseMapping();

        TableMapping GetTableMapping(Type entityType);
    }

    public static class MappingProviderExtensions
    {
        public static TableMapping GetTableMapping<T>(this IMappingProvider prov) where T : class
        {
            return prov.GetTableMapping(typeof (T));
        }
    }
}
