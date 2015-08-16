using System;
using System.Linq.Expressions;
using System.Reflection;
using TagKid.Framework.Exceptions;

namespace TagKid.Framework.Validation
{
    public class PropertyValidatorBuilder<TEntity, TProperty> : IPropertyValidatorBuilder<TEntity, TProperty>
    {
        private readonly PropertyInfo[] _propChain;
        private readonly CompositeValidator _validator;

        public PropertyValidatorBuilder(Expression<Func<TEntity, TProperty>> propExpression)
        {
            var resolver = new PropertyChainResolver(propExpression);
            _propChain = resolver.Resolve();
            _validator = new CompositeValidator();
        }

        public IPropertyValidatorBuilder<TEntity, TProperty> AddRule(IValidationRule rule, Error error)
        {
            _validator.AddValidator(new PropertyValidator(_propChain, rule, error));
            return this;
        }

        public IValidator Build()
        {
            return _validator;
        }
    }
}