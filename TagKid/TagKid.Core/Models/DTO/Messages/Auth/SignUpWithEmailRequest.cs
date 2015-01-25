using Taga.Core.Validation;
using TagKid.Core.Exceptions;
using TagKid.Core.Utils;

namespace TagKid.Core.Models.DTO.Messages.Auth
{
    public class SignUpWithEmailRequest
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
    }

    public class SignUpWithEmailRequestValidator : Validator<SignUpWithEmailRequest>
    {
        protected override void BuildRules()
        {
            RuleFor(r => r.Email)
                .Email(Errors.V_EmailAddress)
                .Length(Errors.V_EmailAddress, 5, 80);

            RuleFor(r => r.Username)
                .NotNull(Errors.V_Username)
                .Length(Errors.V_Username, 1, 16)
                .Charset(Errors.V_Username, "abcdefghijklmnopqrstuvwxyz0123456789-_", false, Cultures.EnGb);

            RuleFor(r => r.Password)
                .NotNull(Errors.V_Password)
                .Length(Errors.V_Password, 6, 20);

            RuleFor(r => r.Fullname)
                .NotNull(Errors.V_Fullname)
                .Length(Errors.V_Fullname, 0, 50);
        }
    }
}
