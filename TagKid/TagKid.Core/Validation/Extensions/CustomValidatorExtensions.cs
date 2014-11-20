using FluentValidation;
using TagKid.Core.Exceptions;

namespace TagKid.Core.Validation.Extensions
{
    public static class CustomValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> TrimmedLength<T>(this IRuleBuilder<T, string> ruleBuilder, int min,
            int max, Error error)
        {
            return ruleBuilder.SetValidator(new TrimmedLengthValidator(min, max)).WithState(t => error);
        }

        public static IRuleBuilderOptions<T, string> Charset<T>(this IRuleBuilder<T, string> ruleBuilder, string charset,
            bool caseSensitive, string culture, Error error)
        {
            return ruleBuilder.SetValidator(new CharsetValidator(charset, caseSensitive, culture)).WithState(t => error);
        }

        public static IRuleBuilderOptions<T, TProperty> NotNull<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, Error error)
        {
            return ruleBuilder.NotNull().WithState(t => error);
        }

        public static IRuleBuilderOptions<T, string> Length<T>(this IRuleBuilder<T, string> ruleBuilder, int min,
            int max, Error error)
        {
            return ruleBuilder.Length(min, max).WithState(t => error);
        }

        public static IRuleBuilderOptions<T, string> EmailAddress<T>(this IRuleBuilder<T, string> ruleBuilder, Error error)
        {
            return ruleBuilder.EmailAddress().WithState(t => error);
        }
    }
}