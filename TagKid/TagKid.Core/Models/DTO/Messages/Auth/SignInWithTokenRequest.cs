using Taga.Core.Validation;
using TagKid.Core.Exceptions;

namespace TagKid.Core.Models.DTO.Messages.Auth
{
    public class SignInWithTokenRequest
    {
        public long TokenId { get; set; }
        public string Token { get; set; }
    }

    public class SignInWithTokenRequestValidator : Validator<SignInWithTokenRequest>
    {
        protected override void BuildRules()
        {
            RuleFor(r => r.TokenId)
                .GreaterThan(Errors.V_TokenId, 0);

            RuleFor(r => r.Token)
                .Guid(Errors.V_Token);
        }
    }
}
