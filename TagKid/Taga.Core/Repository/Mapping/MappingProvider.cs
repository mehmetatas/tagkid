using System;
using System.Linq;

namespace Taga.Core.Repository.Mapping
{
    public class MappingProvider : IMappingProvider
    {
        private DatabaseMapping _databaseMapping;

        public void SetDatabaseMapping(DatabaseMapping databaseMapping)
        {
            _databaseMapping = databaseMapping;
        }

        public DatabaseMapping GetDatabaseMapping()
        {
            return _databaseMapping;
        }

        public TableMapping GetTableMapping(Type entityType)
        {
            return _databaseMapping[entityType];
        }
    }
}