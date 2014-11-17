using TagKid.Core.Models.DTO.Messages.Auth;

namespace TagKid.Core.Service
{
    public interface IAuthService : ITagKidService
    {
        SignUpResponse SignUpWithEmail(SignUpWithEmailRequest request);
    }
}