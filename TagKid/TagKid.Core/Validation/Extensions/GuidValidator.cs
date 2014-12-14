using FluentValidation.Resources;
using FluentValidation.Validators;
using System;

namespace TagKid.Core.Validation.Extensions
{
    public class GuidValidator : PropertyValidator
    {
        public GuidValidator()
            : base(() => Messages.regex_error)
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            Guid dummy;
            return Guid.TryParse((string)context.PropertyValue, out dummy);
        }
    }
}
