using Taga.Core.Validation;
using TagKid.Core.Exceptions;
using TagKid.Core.Utils;

namespace TagKid.Core.Models.DTO.Messages.Auth
{
    public class ActivateAccountRequest
    {
        public long ConfirmationCodeId { get; set; }
        public string ConfirmationCode { get; set; }
    }

    public class ActivateAccountRequestValidator : Validator<ActivateAccountRequest>
    {
        protected override void BuildRules()
        {
            RuleFor(r => r.ConfirmationCodeId)
                .GreaterThan(Errors.V_ConfirmationCodeId, 0);

            RuleFor(r => r.ConfirmationCode)
                .Length(Errors.V_ConfirmationCode, 64, 64)
                .Charset(Errors.V_ConfirmationCode, "0123456789abcdef", false, Cultures.EnGb);
        }
    }
}
