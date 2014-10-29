using System;
using System.Data;
using Taga.Core.IoC;
using Taga.Core.Repository.Mapping;

namespace Taga.Repository.Hybrid.Commands
{
    abstract class BaseCommand : IHybridUowCommand
    {
        public static readonly IMappingProvider MappingProvider = ServiceProvider.Provider.GetOrCreate<IMappingProvider>();
        public static readonly IHybridDbProvider HybridDbProvider = ServiceProvider.Provider.GetOrCreate<IHybridDbProvider>();

        protected readonly object Entity;
        protected readonly Type EntityType;

        protected BaseCommand(object entity)
        {
            Entity = entity;
            EntityType = entity.GetType();
        }

        public abstract void Execute(IDbConnection conn);

        protected static string GetParamName(string columnName)
        {
            return String.Format("{0}p_{1}", HybridDbProvider.ParameterPrefix, columnName);
        }
    }
}