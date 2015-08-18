using System;
using TagKid.Core.Models.Database;

namespace TagKid.Core.Domain
{
    public interface IAuthDomain
    {
        void Register(string email, string username, string password);
        void ActivateRegistration(long id, Guid token);
        User LoginWithPassword(string emailOrUsername, string password);
    }
}
