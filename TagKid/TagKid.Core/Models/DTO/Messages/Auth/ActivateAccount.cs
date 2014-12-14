using TagKid.Core.Exceptions;
using TagKid.Core.Validation;
using TagKid.Core.Validation.Extensions;

namespace TagKid.Core.Models.DTO.Messages.Auth
{
    public class ActivateAccountResponse : Response
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string ProfileImageUrl { get; set; }
    }

    public class ActivateAccountRequest : Request
    {
        public long ConfirmationCodeId { get; set; }
        public string ConfirmationCode { get; set; }
    }

    public class ActivateAccountRequestValidator : TagKidValidator<ActivateAccountRequest>
    {
        public ActivateAccountRequestValidator()
        {
            RuleFor(r => r.ConfirmationCodeId)
                .GreaterThan(0, Errors.V_ConfirmationCodeId);

            RuleFor(r => r.ConfirmationCode)
                .TrimmedLength(64, 64, Errors.V_ConfirmationCode)
                .Charset("0123456789abcdef", false, "en-GB", Errors.V_ConfirmationCode);
        }
    }
}
