using TagKid.Core.Exceptions;
using TagKid.Core.Validation;
using TagKid.Core.Validation.Extensions;

namespace TagKid.Core.Models.DTO.Messages.Auth
{
    public class SignInWithTokenResponse : Response
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string ProfileImageUrl { get; set; }
    }

    public class SignInWithTokenRequest : Request
    {
        public long TokenId { get; set; }
        public string Token { get; set; }
    }

    public class SignInWithTokenRequestValidator : TagKidValidator<SignInWithTokenRequest>
    {
        public SignInWithTokenRequestValidator()
        {
            RuleFor(r => r.TokenId)
                .GreaterThan(0, Errors.V_TokenId);

            RuleFor(r => r.Token)
                .Guid(Errors.V_Token);
        }
    }
}
