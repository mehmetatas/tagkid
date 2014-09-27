using FluentValidation;
using System;
using TagKid.Lib.Models.DTO.Messages;
using TagKid.Lib.Validation.Extensions;

namespace TagKid.Lib.Validation
{
    public class SignUpValidator : AbstractValidator<SignUpRequest>
    {
        public SignUpValidator()
        {
            RuleFor(r => r.Username)
                .Cascade(CascadeMode.Continue)
                .TrimmedLength(3, 20).WithMessage("Username must be 3-20 characters long");

            RuleFor(r => r.Email)
                .Cascade(CascadeMode.Continue)
                .TrimmedLength(5, 100)
                .EmailAddress()
                .When(r => String.IsNullOrWhiteSpace(r.FacebookId))
                .WithMessage("Invalid Email");

            RuleFor(r => r.Password)
                .Cascade(CascadeMode.Continue)
                .Length(6, 16)
                .When(r => String.IsNullOrWhiteSpace(r.FacebookId))
                .WithMessage("Password must be 6-16 characters long");
        }
    }
}
