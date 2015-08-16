using System;
using TagKid.Core.Exceptions;
using TagKid.Core.Models.Database;
using TagKid.Core.Providers;
using TagKid.Core.Repository;

namespace TagKid.Core.Domain.Impl
{
    public class AuthDomain : IAuthDomain
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfirmationCodeRepository _confRepo;
        private readonly ICryptoProvider _crypto;

        public AuthDomain(IUserRepository userRepo, IConfirmationCodeRepository confRepo, ICryptoProvider crypto)
        {
            _userRepo = userRepo;
            _confRepo = confRepo;
            _crypto = crypto;
        }

        public void Register(string fullname, string email, string username, string password)
        {
            var user = _userRepo.GetByEmail(email);
            if (user != null)
            {
                throw Errors.Auth_EmailAlreadyExists;
            }

            user = _userRepo.GetByUsername(username);
            if (user != null)
            {
                throw Errors.Auth_UsernameAlreadyExists;
            }

            user = new User
            {
                Fullname = fullname,
                Email = email,
                Username = username,
                Password = _crypto.ComputeHash(password),
                JoinDate = DateTime.UtcNow,
                Type = UserType.User,
                Status = UserStatus.AwaitingActivation
            };

            _userRepo.Save(user);

            var confCode = new ConfirmationCode
            {
                Code = Guid.NewGuid(),
                ExpireDate = DateTime.UtcNow.AddDays(30),
                Reason = ConfirmationReason.Registration,
                SendDate = DateTime.UtcNow,
                User = user,
                Status = ConfirmationCodeStatus.AwaitingConfirmation
            };

            _confRepo.Save(confCode);
        }
        
        public User LoginWithPassword(string emailOrUsername, string password)
        {
            var user = emailOrUsername.Contains("@")
                ? _userRepo.GetByEmail(emailOrUsername)
                : _userRepo.GetByUsername(emailOrUsername);

            if (user == null)
            {
                throw Errors.Auth_InvalidLogin;
            }

            var pwdHash = _crypto.ComputeHash(password);

            if (pwdHash != user.Password)
            {
                throw Errors.Auth_InvalidLogin;
            }

            if (user.Status == UserStatus.AwaitingActivation)
            {
                throw Errors.Auth_UserAwaitingActivation;
            }

            if (user.Status == UserStatus.Banned)
            {
                throw Errors.Auth_UserBanned;
            }

            if (user.Status == UserStatus.Passive)
            {
                throw Errors.Auth_UserInactive;
            }

            return user;
        }
    }
}
