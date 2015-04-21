﻿using System;
using System.Linq.Expressions;
using FluentNHibernate.Mapping;
using TagKid.Framework.IoC;
using TagKid.Framework.Repository.Mapping;

namespace TagKid.Framework.Repository.Impl
{
    public abstract class AutoClassMap<T> : ClassMap<T> where T : class
    {
        protected AutoClassMap()
        {
            var mappingProv = DependencyContainer.Current.Resolve<IMappingProvider>();

            var dbSystem = mappingProv.GetDatabaseMapping().DbSystem;

            var tableMapping = mappingProv.GetTableMapping<T>();

            Table(tableMapping.TableName);

            CompositeIdentityPart<T> compositeIdPart = null;

            var hasCompositeKey = tableMapping.IdColumns.Length > 1;

            foreach (var columnMapping in tableMapping.Columns)
            {
                var propInf = columnMapping.PropertyInfo;

                var parameter = Expression.Parameter(tableMapping.Type, "entity");
                var property = Expression.Property(parameter, propInf);
                var body = Expression.Convert(property, typeof(object));
                var funcType = typeof(Func<T, object>);
                var lambda = (Expression<Func<T, object>>)Expression.Lambda(funcType, body, parameter);

                var columnName = columnMapping.ColumnName;

                if (columnMapping.IsId)
                {
                    if (hasCompositeKey)
                    {
                        if (compositeIdPart == null)
                        {
                            compositeIdPart = CompositeId();
                        }

                        compositeIdPart.KeyProperty(lambda, columnName);
                    }
                    else
                    {
                        var idPart = Id(lambda, columnName);

                        if (columnMapping.IsAutoIncrement)
                        {
                            if (dbSystem == DbSystem.Oracle)
                            {
                                idPart.GeneratedBy.SequenceIdentity("SEQ_" + tableMapping.TableName);
                            }
                            else
                            {
                                idPart.GeneratedBy.Identity();
                            }
                        }
                        else
                        {
                            idPart.GeneratedBy.Assigned();
                        }
                    }
                }
                else
                {
                    var propPart = Map(lambda, columnName);

                    if (propInf.PropertyType.IsEnum)
                    {
                        propPart.CustomType(propInf.PropertyType);
                    }
                }
            }
        }
    }
}
