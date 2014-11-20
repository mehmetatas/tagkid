using TagKid.Core.Exceptions;
using TagKid.Core.Utils;
using TagKid.Core.Validation;
using TagKid.Core.Validation.Extensions;

namespace TagKid.Core.Models.DTO.Messages.Auth
{
    public class SignUpWithEmailResponse : Response
    {
    }

    public class SignUpWithEmailRequest : Request
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
    }

    public class SignUpWithEmailRequestValidator : TagKidValidator<SignUpWithEmailRequest>
    {
        public SignUpWithEmailRequestValidator()
        {
            RuleFor(r => r.Email)
                .EmailAddress(Errors.Validation_SignUp_EmailAddress)
                .TrimmedLength(5, 80, Errors.Validation_SignUp_EmailAddress);

            RuleFor(r => r.Username)
                .NotNull(Errors.Validation_SignUp_Username)
                .TrimmedLength(1, 16, Errors.Validation_SignUp_Username)
                .Charset("abcdefghijklmnopqrstuvwxyz0123456789-_", false, Cultures.EnGb,
                    Errors.Validation_SignUp_Username);

            RuleFor(r => r.Password)
                .NotNull(Errors.Validation_SignUp_Password)
                .TrimmedLength(6, 20, Errors.Validation_SignUp_Password);

            RuleFor(r => r.Fullname)
                .Length(0, 50, Errors.Validation_SignUp_Fullname);
        }
    }
}
