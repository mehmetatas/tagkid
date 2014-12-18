using TagKid.Core.Exceptions;
using TagKid.Core.Validation;
using TagKid.Core.Validation.Extensions;

namespace TagKid.Core.Models.DTO.Messages.Auth
{
    public class SignInWithPasswordRequest
    {
        public string Password { get; set; }
        public string EmailOrUsername { get; set; }
    }

    public class SignInWithPasswordRequestValidator : TagKidValidator<SignInWithPasswordRequest>
    {
        public SignInWithPasswordRequestValidator()
        {
            RuleFor(r => r.Password)
                .NotNull(Errors.V_Password);

            RuleFor(r => r.EmailOrUsername)
                .NotNull(Errors.V_UsernameOrEmail);
        }
    }
}
