using TagKid.Lib.Entities;

namespace TagKid.Lib.Services
{
    public interface IAuthService : ITagKidService
    {
        void SignUp(User user);

        User SignIn(string emailOrUsername, string password);
        
        void SignUpWithFacebook(User user, string facebookAccessToken);

        User SignInWithFacebook(string facebookId, string facebookAccessToken);
    }
}
