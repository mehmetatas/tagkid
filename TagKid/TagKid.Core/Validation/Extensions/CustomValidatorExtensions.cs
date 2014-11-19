using FluentValidation;

namespace TagKid.Core.Validation.Extensions
{
    public static class CustomValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> TrimmedLength<T>(this IRuleBuilder<T, string> ruleBuilder, int min,
            int max)
        {
            return ruleBuilder.SetValidator(new TrimmedLengthValidator(min, max));
        }

        public static IRuleBuilderOptions<T, string> Charset<T>(this IRuleBuilder<T, string> ruleBuilder, string charset, bool caseSensitive, string culture)
        {
            return ruleBuilder.SetValidator(new CharsetValidator(charset, caseSensitive, culture));
        }
    }
}