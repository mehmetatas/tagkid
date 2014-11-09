using System;
using FluentValidation;
using TagKid.Core.Models.DTO.Messages;

namespace TagKid.Core.Validation
{
    public class SignInValidator : AbstractValidator<SignInRequest>
    {
        public SignInValidator()
        {
            RuleFor(r => r.EmailOrUsername)
                .Cascade(CascadeMode.Continue)
                .NotEmpty()
                .When(r => String.IsNullOrWhiteSpace(r.FacebookId))
                .WithMessage("Username/Email cannot be empty");

            RuleFor(r => r.Password)
                .Cascade(CascadeMode.Continue)
                .NotEmpty()
                .When(r => String.IsNullOrWhiteSpace(r.FacebookId))
                .WithMessage("Password cannot be empty");
        }
    }
}