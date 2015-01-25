using Taga.Core.DynamicProxy;
using TagKid.Core.Domain;
using TagKid.Core.Models;
using TagKid.Core.Models.DTO.Messages;
using TagKid.Core.Models.DTO.Messages.User;
using TagKid.Core.Service;

namespace TagKid.Service
{
    [Intercept]
    public class UserService : IUserService
    {
        private readonly IUserDomainService _userDomain;

        public UserService(IUserDomainService userDomain)
        {
            _userDomain = userDomain;
        }

        public Response GetProfile(GetProfileRequest request)
        {
            var profile = _userDomain.GetProfile(request.Username);
            return Response.Success.WithData(profile);
        }
    }
}