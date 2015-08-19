using System;
using TagKid.Framework.Exceptions;
using TagKid.Framework.Validation;

namespace TagKid.Core.Models.Messages.ValidationExtensions
{
    public static class ValidationExtensions
    {
        public static IPropertyValidatorBuilder<T, string> Password<T>(this IPropertyValidatorBuilder<T, string> ruleBuilder, Error error)
        {
            return ruleBuilder.AddRule(new PasswordRule(), error);
        }
    }

    public class PasswordRule : IValidationRule
    {
        public bool Execute(object obj)
        {
            var pwd = (string) obj;

            if (String.IsNullOrWhiteSpace(pwd) || pwd.Length < 6 || pwd.Length > 32)
            {
                return false;
            }

            return true;
        }
    }
}
