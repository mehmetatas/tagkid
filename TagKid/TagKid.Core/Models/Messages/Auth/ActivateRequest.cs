using System;
using TagKid.Core.Exceptions;
using TagKid.Framework.Validation;

namespace TagKid.Core.Models.Messages.Auth
{
    public class ActivateRegistrationRequest
    {
        public long Id { get; set; }
        public Guid Token { get; set; }
    }

    public class ActivateRegistrationRequestValidator : Validator<ActivateRegistrationRequest>
    {
        protected override void BuildRules()
        {
            RuleFor(r => r.Id).GreaterThan(Errors.Auth_InvalidConfirmationCodeId, 0);
        }
    }
}