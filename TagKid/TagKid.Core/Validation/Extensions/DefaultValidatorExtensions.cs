using FluentValidation;

namespace TagKid.Core.Validation.Extensions
{
    public static class DefaultValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> TrimmedLength<T>(this IRuleBuilder<T, string> ruleBuilder, int min,
            int max)
        {
            return ruleBuilder.SetValidator(new TrimmedLengthValidator(min, max));
        }
    }
}