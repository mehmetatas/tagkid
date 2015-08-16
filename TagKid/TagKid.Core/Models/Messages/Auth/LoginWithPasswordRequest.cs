using TagKid.Core.Exceptions;
using TagKid.Framework.Validation;

namespace TagKid.Core.Models.Messages.Auth
{
    public class LoginWithPasswordRequest
    {
        public string EmailOrUsername { get; set; }
        public string Password { get; set; }
    }

    public class LoginWithPasswordRequestValidator : Validator<LoginWithPasswordRequest>
    {
        protected override void BuildRules()
        {
            RuleFor(r => r.EmailOrUsername).NotEmpty(Errors.Auth_EmailOrUsernameCannotBeEmpty);
            RuleFor(r => r.Password).NotEmpty(Errors.Auth_PasswordPolicyError);
        }
    }
}
