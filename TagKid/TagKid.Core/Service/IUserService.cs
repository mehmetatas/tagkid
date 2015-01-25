using TagKid.Core.Models.DTO.Messages;
using TagKid.Core.Models.DTO.Messages.User;

namespace TagKid.Core.Service
{
    public interface IUserService
    {
        Response GetProfile(GetProfileRequest request);
    }
}