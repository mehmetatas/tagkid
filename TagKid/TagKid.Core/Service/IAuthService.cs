using TagKid.Core.Models.DTO.Messages.Auth;

namespace TagKid.Core.Service
{
    public interface IAuthService : ITagKidService
    {
        SignUpWithEmailResponse SignUpWithEmail(SignUpWithEmailRequest request);

        SignInWithPasswordResponse SignInWithPassword(SignInWithPasswordRequest request);

        ActivateAccountResponse ActivateAccount(long ccid, string cc);
    }
}