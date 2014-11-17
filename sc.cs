
    public abstract class BaseSubclassMap<T> : SubclassMap<T> where T : class
    {
        protected BaseSubclassMap()
        {
            InitMappings();
        }

        protected virtual void InitMappings()
        {
            Table(typeof(T).Name);
            InitColumnMappings();
            InitDiscriminatorValue();
        }

        protected virtual void InitColumnMappings()
        {
            Join(typeof(T).Name, InitJoinMappings);
        }

        protected virtual void InitJoinMappings(JoinPart<T> joinPart)
        {
            joinPart.Fetch.Join();
            joinPart.KeyColumn("Id");

            var entityType = typeof(T);
            foreach (var propInf in entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                if (propInf.Name == "Id")
                {
                    continue;
                }

                if (propInf.PropertyType.IsGenericType && propInf.PropertyType.GetGenericTypeDefinition() == typeof(IList<>))
                {
                    continue;
                }

                if (IsHidingByNew(propInf))
                {
                    continue;
                }

                var lambda = CreatePropertyLambda(propInf);
                if (propInf.PropertyType.IsValueType || propInf.PropertyType == typeof(string))
                {
                    var propPart = joinPart.Map(lambda);

                    if (propInf.PropertyType.IsEnum)
                    {
                        propPart.CustomType(propInf.PropertyType);
                    }

                    OnPropertyMap(propInf, propPart);
                }
                else
                {
                    var refProp = joinPart.References(lambda)
                        .Class(propInf.PropertyType);

                    if (propInf.Name == propInf.PropertyType.Name)
                    {
                        refProp.Columns(propInf.Name + "Id");
                    }
                    else
                    {
                        refProp.Columns(propInf.Name);
                    }
                }
            }

            if (MappingHelper.IsFetchValueSelect)
            {
                joinPart.Fetch.Select();
            }
        }

        /// <summary>
        /// Checks if the given property hides a base property with 'new' keyword
        /// </summary>
        private bool IsHidingByNew(PropertyInfo propInf)
        {
            var count = propInf.DeclaringType.GetProperties().Count(pi => propInf.Name == pi.Name);

            if (count == 1)
            {
                return false;
            }

            return propInf.DeclaringType == typeof(T);
        }

        /// <summary>
        /// This method is called after mapping a property to a column
        /// </summary>
        /// <param name="propInf">PropertyInfo of the mapped property</param>
        /// <param name="propertyPart">PropertyPart for chaining new mapping attributes</param>
        protected virtual void OnPropertyMap(PropertyInfo propInf, PropertyPart propertyPart)
        {

        }

        /// <summary>
        /// Initializes discriminator values for sub-class
        /// </summary>
        protected virtual void InitDiscriminatorValue()
        {
            var entityType = typeof(T);
            Type discClass = null;
            var baseType = entityType.BaseType;
            var discFieldName = entityType.Name;

            while (baseType != typeof(object))
            {
                discClass = typeof(Discriminators).GetNestedType(baseType.Name);
                if (discClass != null)
                {
                    break;
                }
                discFieldName = baseType.Name + "_" + discFieldName;
                baseType = baseType.BaseType;
            }

            if (baseType == typeof(object))
            {
                throw new Exception(String.Format("Discriminator value for {0} not found!", entityType));
            }

            var discField = discClass.GetField(discFieldName);

            if (discField == null)
            {
                throw new Exception(String.Format("Discriminator value for {0} not found!", entityType));
            }

            var discValue = (int)discField.GetValue(null);
            DiscriminatorValue(discValue);
        }

        /// <summary>
        ///     Creates a lamda expression for the given property such that x => x.Property
        /// </summary>
        /// <param name="propInf">Property for creating expression</param>
        /// <returns>t => t.Property</returns>
        private Expression<Func<T, object>> CreatePropertyLambda(PropertyInfo propInf)
        {
            var parameter = Expression.Parameter(typeof(T), "entity");
            var property = Expression.Property(parameter, propInf);
            var body = Expression.Convert(property, typeof(object));
            var funcType = typeof(Func<T, object>);
            var lambda = (Expression<Func<T, object>>)Expression.Lambda(funcType, body, parameter);
            return lambda;
        }
    }
}
