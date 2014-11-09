using System;
using FluentValidation;
using TagKid.Core.Models.DTO.Messages;
using TagKid.Core.Validation.Extensions;

namespace TagKid.Core.Validation
{
    public class SignUpValidator : AbstractValidator<SignUpRequest>
    {
        public SignUpValidator()
        {
            RuleFor(r => r.User.Username)
                .Cascade(CascadeMode.Continue)
                .TrimmedLength(3, 20)
                .WithMessage("Username must be 3-20 characters long");

            RuleFor(r => r.User.Email)
                .Cascade(CascadeMode.Continue)
                .TrimmedLength(5, 100)
                .EmailAddress()
                .When(r => String.IsNullOrWhiteSpace(r.User.FacebookId))
                .WithMessage("Invalid Email");

            RuleFor(r => r.User.Password)
                .Cascade(CascadeMode.Continue)
                .Length(6, 16)
                .When(r => String.IsNullOrWhiteSpace(r.User.FacebookId))
                .WithMessage("Password must be 6-16 characters long");
        }
    }
}