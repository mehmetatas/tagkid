using TagKid.Core.Exceptions;
using TagKid.Core.Models.Messages.ValidationExtensions;
using TagKid.Framework.Validation;

namespace TagKid.Core.Models.Messages.Auth
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class RegisterRequestValidator : Validator<RegisterRequest>
    {
        protected override void BuildRules()
        {
            RuleFor(r => r.Username).NotEmpty(Errors.Auth_UsernameCannotBeEmpty);
            RuleFor(r => r.Email).Email(Errors.Auth_InvalidEmailAddress);
            RuleFor(r => r.Password).Password(Errors.Auth_PasswordPolicyError);
        }
    }
}
