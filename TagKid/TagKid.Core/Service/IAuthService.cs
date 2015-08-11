using TagKid.Core.Models.Messages.Auth;
using TagKid.Framework.WebApi;

namespace TagKid.Core.Service
{
    public interface IAuthService
    {
        Response Signup(SignupRequest request);
    }
}
