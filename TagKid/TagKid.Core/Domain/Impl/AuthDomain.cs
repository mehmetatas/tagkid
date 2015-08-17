using System;
using TagKid.Core.Exceptions;
using TagKid.Core.Models.Database;
using TagKid.Core.Providers;
using TagKid.Core.Repository;

namespace TagKid.Core.Domain.Impl
{
    public class AuthDomain : IAuthDomain
    {
        // TODO: Move to config
        private const int RegistrationConfCoedTimeoutDays = 30;

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
                ExpireDate = DateTime.UtcNow.AddDays(RegistrationConfCoedTimeoutDays),
                Reason = ConfirmationReason.Registration,
                SendDate = DateTime.UtcNow,
                User = user,
                Status = ConfirmationCodeStatus.AwaitingConfirmation
            };

            _confRepo.Save(confCode);
        }

        public void ActivateRegistration(long id, Guid token)
        {
            var confCode = _confRepo.GetForActivation(id);

            if (confCode == null)
            {
                throw Errors.Auth_ConfirmationCodeNotFound;
            }

            if (confCode.Code != token)
            {
                throw Errors.Auth_ConfirmationCodeMismatch;
            }

            if (confCode.Status == ConfirmationCodeStatus.Confirmed)
            {
                throw Errors.Auth_ConfirmationCodeAlreadyConfirmed;
            }

            if (confCode.Status == ConfirmationCodeStatus.Expired)
            {
                throw Errors.Auth_ConfirmationCodeExpired;
            }

            if (confCode.ExpireDate < DateTime.UtcNow)
            {
                // TODO: Exception will cause rollback and confCode will never be updated.
                confCode.Status = ConfirmationCodeStatus.Expired;
                _confRepo.Save(confCode);
                throw Errors.Auth_ConfirmationCodeExpired;
            }

            if (confCode.Reason != ConfirmationReason.Registration)
            {
                throw Errors.Auth_ConfirmationCodeReasonMismatch;
            }

            var user = confCode.User;

            if (user.Status != UserStatus.AwaitingActivation)
            {
                throw Errors.Auth_UserNotAwaitingActivation;
            }

            confCode.Status = ConfirmationCodeStatus.Confirmed;
            confCode.ConfirmDate = DateTime.UtcNow;
            _confRepo.Save(confCode);

            user.Status = UserStatus.Active;
            _userRepo.Save(user);
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
