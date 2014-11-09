using TagKid.Core.Models.DTO.Messages;

namespace TagKid.Core.Services
{
    public interface IAuthService : ITagKidService
    {
        SignUpResponse SignUp(SignUpRequest request);

        SignInResponse SignIn(SignInRequest request);

        SignInResponse SignInWithToken(long tokenId, string guid);

        void ValidateRequest(Request request);
    }
}