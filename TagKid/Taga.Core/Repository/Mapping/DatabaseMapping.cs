using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Taga.Core.Repository.Mapping
{
    public class DatabaseMapping
    {
        private readonly IDatabaseNamingConvention _namingConvention;

        private readonly Dictionary<Type, TableMapping> _tableMappings;

        private TableMapping _currentTableMapping;

        private DatabaseMapping(IDatabaseNamingConvention namingConvention)
        {
            _namingConvention = namingConvention;
            _tableMappings = new Dictionary<Type, TableMapping>();
        }

        public DatabaseMapping Map(Type entityType, params string[] idProperties)
        {
            _currentTableMapping = TableMapping.For(entityType, _namingConvention, idProperties);
            _tableMappings.Add(entityType, _currentTableMapping);
            return this;
        }

        public DatabaseMapping ToTable(string tableName)
        {
            _currentTableMapping.TableName = tableName;
            return this;
        }

        public IEnumerable<TableMapping> TableMappings
        {
            get { return _tableMappings.Values; }
        } 

        public TableMapping this[Type entityType]
        {
            get
            {
                var type = entityType;
                while (type != typeof(object))
                {
                    if (_tableMappings.ContainsKey(type))
                    {
                        return _tableMappings[type];        
                    }
                    type = type.BaseType;
                }
                throw new InvalidOperationException("Unknown entity type: " + entityType);
            }
        }

        public static DatabaseMapping WithNamingConvention(IDatabaseNamingConvention namingConvention)
        {
            return new DatabaseMapping(namingConvention);
        }
    }

    public static class DatabaseMappingExtensions
    {
        public static DatabaseMapping Map<T>(this DatabaseMapping databaseMapping, params Expression<Func<T, object>>[] propExpressions)
        {
            var propNames = new List<string>();
            
            foreach (var propExpression in propExpressions)
            {
                MemberExpression memberExpression;
                if (propExpression.Body is UnaryExpression)
                {
                    var unaryExp = (UnaryExpression) propExpression.Body;
                    memberExpression = (MemberExpression) unaryExp.Operand;
                }
                else
                {
                    memberExpression = (MemberExpression) propExpression.Body;
                }

                var propInf = (PropertyInfo) memberExpression.Member;

                propNames.Add(propInf.Name);
            }

            return databaseMapping.Map(typeof(T), propNames.ToArray());
        }
    }
}