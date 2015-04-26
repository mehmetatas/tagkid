using TagKid.Core.Models.Messages;
using TagKid.Core.Models.Messages.Auth;

namespace TagKid.Core.Service
{
    public interface IAuthService
    {
        Response Signup(SignupRequest request);
    }
}
