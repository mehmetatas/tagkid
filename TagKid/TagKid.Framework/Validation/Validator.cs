using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TagKid.Framework.Validation
{
    public abstract class Validator<T> : IValidator, IJavascriptValidation
    {
        private readonly List<IValidator> _validators;
        private readonly IValidationRuleBuilder<T> _ruleBuilder;

        protected Validator()
        {
            _ruleBuilder = new ValidationRuleBuilder<T>();
            BuildRules();
            _validators = _ruleBuilder.Build();
        }

        protected PropertyValidatorBuilder<T, TProperty> RuleFor<TProperty>(Expression<Func<T, TProperty>> propExpression)
        {
            return _ruleBuilder.RuleFor(propExpression);
        }

        ValidationResult IValidator.Validate(object obj)
        {
            foreach (var validator in _validators)
            {
                var res = validator.Validate(obj);
                if (!res.IsValid)
                {
                    return res;
                }
            }
            return ValidationResult.Successful;
        }

        public string BuildValidationScript(IValidationScriptBuilder scriptBuilder)
        {
            return scriptBuilder.Build(_validators.OfType<IPropertyValidator>());
        }

        protected abstract void BuildRules();
    }
}