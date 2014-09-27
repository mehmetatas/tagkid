using TagKid.Lib.Models.DTO.Messages;

namespace TagKid.Lib.Services
{
    public interface IAuthService : ITagKidService
    {
        SignUpResponse SignUp(SignUpRequest request);

        SignInResponse SignIn(SignInRequest request);

        SignInResponse SignInWithToken(long tokenId, string guid);
    }
}
