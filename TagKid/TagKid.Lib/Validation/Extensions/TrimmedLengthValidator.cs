using System;
using System.Linq.Expressions;
using FluentValidation.Resources;
using FluentValidation.Validators;

namespace TagKid.Lib.Validation.Extensions
{
    internal class TrimmedLengthValidator : PropertyValidator, ILengthValidator
    {
        public TrimmedLengthValidator(int min, int max)
            : this(min, max, () => Messages.length_error)
        {
        }

        public TrimmedLengthValidator(int min, int max, Expression<Func<string>> errorMessageResourceSelector)
            : base(errorMessageResourceSelector)
        {
            if (max != -1 && max < min)
                throw new ArgumentOutOfRangeException("max", "Max should be larger than min.");

            Max = max;
            Min = min;
        }

        public int Min { get; private set; }
        public int Max { get; private set; }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue == null)
                return true;

            var length = context.PropertyValue.ToString().Trim().Length;

            if (length < Min || (length > Max && Max != -1))
            {
                context.MessageFormatter.AppendArgument("MinLength", Min)
                    .AppendArgument("MaxLength", Max)
                    .AppendArgument("TotalLength", length);
                return false;
            }
            return true;
        }
    }
}