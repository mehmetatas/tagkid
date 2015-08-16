using System.Reflection;
using TagKid.Framework.Exceptions;

namespace TagKid.Framework.Validation
{
    public class PropertyValidator : IValidator
    {
        private readonly PropertyInfo[] _propInfoChain;
        private readonly IValidationRule _rule;
        private readonly Error _error;

        public PropertyValidator(PropertyInfo[] propInfoChain, IValidationRule rule, Error error)
        {
            _propInfoChain = propInfoChain;
            _rule = rule;
            _error = error;
        }

        public ValidationResult Validate(object instance)
        {
            var value = instance;
            foreach (var propInf in _propInfoChain)
            {
                value = propInf.GetValue(value);
            }

            if (_rule.Execute(value))
            {
                return ValidationResult.Successful;
            }
            return ValidationResult.Failed(_error);
        }
    }
}