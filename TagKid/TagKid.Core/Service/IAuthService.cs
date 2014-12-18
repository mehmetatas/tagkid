using TagKid.Core.Models.DTO.Messages;
using TagKid.Core.Models.DTO.Messages.Auth;

namespace TagKid.Core.Service
{
    public interface IAuthService : ITagKidService
    {
        Response SignOut();

        Response SignUpWithEmail(SignUpWithEmailRequest request);

        Response SignInWithPassword(SignInWithPasswordRequest request);

        Response SignInWithToken(SignInWithTokenRequest request);

        Response ActivateAccount(ActivateAccountRequest request);
    }
}