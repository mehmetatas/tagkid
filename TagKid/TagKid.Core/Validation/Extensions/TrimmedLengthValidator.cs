using FluentValidation.Resources;
using FluentValidation.Validators;
using System;

namespace TagKid.Core.Validation.Extensions
{
    internal class TrimmedLengthValidator : PropertyValidator
    {
        private readonly int _min;
        private readonly int _max;

        public TrimmedLengthValidator(int min, int max)
            : base(() => Messages.length_error)
        {
            if (max != -1 && max < min)
            {
                throw new ArgumentOutOfRangeException("max", "Max should be larger than min.");
            }

            _min = min;
            _max = max;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue == null)
            {
                return _min < 0;
            }

            var length = context.PropertyValue.ToString().Trim().Length;

            return length >= _min && (length <= _max || _max == -1);
        }
    }
}