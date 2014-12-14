using TagKid.Core.Models.DTO.Messages.Auth;

namespace TagKid.Core.Service
{
    public interface IAuthService : ITagKidService
    {
        SignOutResponse SignOut();

        SignUpWithEmailResponse SignUpWithEmail(SignUpWithEmailRequest request);

        SignInWithPasswordResponse SignInWithPassword(SignInWithPasswordRequest request);

        SignInWithTokenResponse SignInWithToken(SignInWithTokenRequest request);

        ActivateAccountResponse ActivateAccount(ActivateAccountRequest request);
    }
}