using System;
using TagKid.Core.Models.Database;

namespace TagKid.Core.Domain
{
    public interface IAuthDomainService : ITagKidDomainService
    {
        void SignUpWithEmail(string email, string username, string password, string fullname);

        void SignUpWithFacebook(string facebookId, string facebookAuthToken);

        User SignInWithPassword(string emailOrUsername, string password);

        User SignInWithFacebook(string facebookId, string facebookAuthToken);

        User SignInWithToken(long tokenId, Guid guid);

        User ActivateAccount(long confirmationCodeId, string code);

        void SignOut();

        void SetupRequestContext(long tokenId, Guid guid);

        void ResetPassword(string email);

        void RequestReactivation(string email);
    }
}
