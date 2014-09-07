using FluentValidation;
using TagKid.Lib.Models.Entities;

namespace TagKid.Lib.Validation
{
    public class SignUpValidator : AbstractValidator<User>
    {
        public SignUpValidator() 
        {
            RuleFor(u => u.Username)
                .Cascade(CascadeMode.Continue)
                .NotEmpty().WithMessage("Empty")
                .Length(3, 20).WithMessage("Invalid Length");

            RuleFor(u => u.Email)
                .EmailAddress().WithMessage("Invalid Email");
        }
    }
}
