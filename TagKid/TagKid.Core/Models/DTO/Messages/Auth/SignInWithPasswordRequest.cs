using Taga.Core.Validation;
using TagKid.Core.Exceptions;

namespace TagKid.Core.Models.DTO.Messages.Auth
{
    public class SignInWithPasswordRequest
    {
        public string Password { get; set; }
        public string EmailOrUsername { get; set; }
    }

    public class SignInWithPasswordRequestValidator : Validator<SignInWithPasswordRequest>
    {
        protected override void BuildRules()
        {
            RuleFor(r => r.Password)
                .NotNull(Errors.V_Password);

            RuleFor(r => r.EmailOrUsername)
                .NotNull(Errors.V_UsernameOrEmail);
        }
    }
}
