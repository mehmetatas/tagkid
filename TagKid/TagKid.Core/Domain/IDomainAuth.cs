
namespace TagKid.Core.Domain
{
    public interface IDomainAuth
    {
        void SignUpWithEmail(string username, string password, string email, string fullname);

        void SignUpWithFacebook(string facebookId, string facebookAuthToken);

        void SignInWithPassword(string emailOrUsername, string password);

        void SignInWithFacebook(string facebookId, string facebookAuthToken);

        void SignInWithToken();

        void ResetPassword(string email);

        void RequestReactivation(string email);

        void ActivateAccount(long tokenId, string guid);

        void ValidateAuthToken();

        void ValidateRequestToken();

        void IssueNewRequestToken();
    }
}
