using TagKid.Core.Domain;
using TagKid.Core.Models.Messages.Auth;
using TagKid.Framework.WebApi;

namespace TagKid.Core.Service.Impl
{
    public class AuthService : IAuthService
    {
        private readonly IAuthDomain _auth;

        public AuthService(IAuthDomain auth)
        {
            _auth = auth;
        }

        public Response Register(RegisterRequest request)
        {
            _auth.Register(request.Fullname, request.Email, request.Username, request.Password);
            return Response.Success;
        }

        public Response LoginWithPassword(LoginWithPasswordRequest request)
        {
            var user = _auth.LoginWithPassword(request.EmailOrUsername, request.Password);
            return Response.Success.WithData(user);
        }
    }
}
