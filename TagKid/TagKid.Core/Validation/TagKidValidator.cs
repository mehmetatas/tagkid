using System;
using System.Linq.Expressions;
using FluentValidation;
using TagKid.Core.Exceptions;
using TagKid.Core.Utils;
using TagKid.Core.Validation.Extensions;

namespace TagKid.Core.Validation
{
    public abstract class TagKidValidator<T> : AbstractValidator<T>
    {
        protected TagKidValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
        }

        protected IRuleBuilderOptions<T, string> Email(Expression<Func<T, string>> prop)
        {
            return RuleFor(prop)
                .EmailAddress()
                .WithState(r => Errors.Validation_EmailAddress)
                .TrimmedLength(5, 80)
                .WithState(r => Errors.Validation_EmailAddress);
        }

        protected IRuleBuilderOptions<T, string> Username(Expression<Func<T, string>> prop)
        {
            return RuleFor(prop)
                .NotNull()
                .WithState(r => Errors.Validation_Username)
                .TrimmedLength(1, 16)
                .WithState(r => Errors.Validation_Username)
                .Charset("abcdefghijklmnopqrstuvwxyz0123456789-_", false, Cultures.EnGb)
                .WithState(r => Errors.Validation_Username);
        }

        protected IRuleBuilderOptions<T, string> Password(Expression<Func<T, string>> prop)
        {
            return RuleFor(prop)
                .NotNull()
                .WithState(r => Errors.Validation_Password)
                .TrimmedLength(6, 20)
                .WithState(r => Errors.Validation_Password);
        }

        protected IRuleBuilderOptions<T, string> Fullname(Expression<Func<T, string>> prop)
        {
            return RuleFor(prop)
                .Length(0, 50)
                .WithState(r => Errors.Validation_Fullname);
        }
    }
}