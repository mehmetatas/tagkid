using System;
using TagKid.Core.Exceptions;
using TagKid.Core.Models.Database;
using TagKid.Core.Repository;

namespace TagKid.Core.Domain.Impl
{
    public class AuthDomain : IAuthDomain
    {
        private readonly IUserRepository _userRepo;

        public AuthDomain(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public void Signup(string fullname, string email, string username, string password)
        {
            var user = _userRepo.GetByEmail(email);
            if (user != null)
            {
                throw Errors.Auth_EmailAlreadyExists.ToException();
            }

            user = _userRepo.GetByUsername(username);
            if (user != null)
            {
                throw Errors.Auth_UsernameAlreadyExists.ToException();
            }

            user = new User
            {
                Fullname = fullname,
                Email = email,
                Username = username,
                Password = password,
                JoinDate = DateTime.UtcNow,
                Type = UserType.User,
                Status = UserStatus.AwaitingActivation
            };

            _userRepo.Save(user);
        }
    }
}
