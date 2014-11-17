using TagKid.Core.Models.Database;

namespace TagKid.Core.Domain
{
    public interface IAuthDomainService : ITagKidDomainService
    {
        void SignUpWithEmail(string email, string username, string password, string fullname);

        void SignUpWithFacebook(string facebookId, string facebookAuthToken);

        void SignInWithPassword(string emailOrUsername, string password);

        void SignInWithFacebook(string facebookId, string facebookAuthToken);

        void SignInWithToken();

        void ResetPassword(string email);

        void RequestReactivation(string email);

        void ActivateAccount(long tokenId, string guid);

        User ValidateAuthToken(long tokenId, string token);

        void ValidateRequestToken(long tokenId, string token);

        void IssueNewRequestToken();
    }
}
