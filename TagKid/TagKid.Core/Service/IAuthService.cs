using TagKid.Core.Models.Messages.Auth;
using TagKid.Framework.Hosting;

namespace TagKid.Core.Service
{
    public interface IAuthService
    {
        Response Register(RegisterRequest request);
        Response ActivateRegistration(ActivateRegistrationRequest request);
        Response LoginWithPassword(LoginWithPasswordRequest request);
    }
}
