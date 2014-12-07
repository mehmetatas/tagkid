using System;
using Taga.Core.DynamicProxy;
using Taga.Core.IoC;
using TagKid.Core.Domain;
using TagKid.Core.Exceptions;
using TagKid.Core.Mailing;
using TagKid.Core.Models;
using TagKid.Core.Models.Database;
using TagKid.Core.Repository;
using TagKid.Core.Utils;
using IServiceProvider = Taga.Core.IoC.IServiceProvider;

namespace TagKid.Domain
{
    [Intercept]
    public class AuthDomainService : IAuthDomainService
    {
        private readonly IServiceProvider _prov;

        public AuthDomainService()
        {
            _prov = ServiceProvider.Provider;
        }

        private IUserRepository UserRepository
        {
            get { return _prov.GetOrCreate<IUserRepository>(); }
        }

        private ITokenRepository TokenRepository
        {
            get { return _prov.GetOrCreate<ITokenRepository>(); }
        }

        private IConfirmationCodeRepository ConfirmationCodeRepository
        {
            get { return _prov.GetOrCreate<IConfirmationCodeRepository>(); }
        }

        private IMailService MailService
        {
            get { return _prov.GetOrCreate<IMailService>(); }
        }

        public virtual void SignUpWithEmail(string email, string username, string password, string fullname)
        {
            var user = UserRepository.GetByEmail(email);
            if (user != null)
            {
                Throw.Critical(Errors.Validation_SignUp_EmailAlreadyExists);
            }

            user = UserRepository.GetByUsername(username);
            if (user != null)
            {
                Throw.Critical(Errors.Validation_SignUp_UsernameAlreadyExists);
            }

            user = new User
            {
                Email = email,
                Username = username,
                Password = Util.EncryptPwd(password),
                Fullname = fullname,
                JoinDate = DateTime.Now,
                Status = UserStatus.AwaitingActivation,
                Type = UserType.User
            };

            UserRepository.Save(user);
            UserRepository.Flush();

            var confirmationCode = new ConfirmationCode
            {
                Code = Util.GenerateConfirmationCode(),
                ExpireDate = DateTime.Now.AddDays(15),
                Reason = ConfirmationReason.NewUser,
                Status = ConfirmationCodeStatus.AwaitingConfirmation,
                SendDate = DateTime.Now,
                UserId = user.Id
            };

            ConfirmationCodeRepository.Save(confirmationCode);

            MailService.SendNewUserActivationMail(user, confirmationCode);
        }

        public virtual void SignUpWithFacebook(string facebookId, string facebookAuthToken)
        {

        }

        public virtual User SignInWithPassword(string emailOrUsername, string password)
        {
            var user = emailOrUsername.Contains("@")
                ? UserRepository.GetByEmail(emailOrUsername)
                : UserRepository.GetByUsername(emailOrUsername);

            if (user == null)
            {
                Throw.Critical(Errors.Security_InvalidUsernameOrPassword, "Invalid email or username: {0}", emailOrUsername);
            }

            if (user.Password != Util.EncryptPwd(password))
            {
                Throw.Critical(Errors.Security_InvalidUsernameOrPassword, "Invalid password for: {0} {1}", user.Username, user.Id);
            }

            if (user.Status != UserStatus.Active)
            {
                Throw.Critical(Errors.Security_InvalidUsernameOrPassword, "User not active: {0} {1}", user.Username, user.Id);
            }

            var authToken = new Token
            {
                Guid = Util.GenerateGuid(),
                Type = TokenType.Auth,
                ExpireDate = DateTime.Now.AddDays(15),
                UserId = user.Id
            };

            TokenRepository.DeleteTokensOfUser(user.Id);
            TokenRepository.Save(authToken);

            RequestContext.Current.NewAuthToken = authToken;

            return user;
        }

        public virtual void SignInWithFacebook(string facebookId, string facebookAuthToken)
        {

        }

        public virtual void SignInWithToken()
        {

        }

        public virtual void ResetPassword(string email)
        {

        }

        public virtual void RequestReactivation(string email)
        {

        }

        public virtual void ActivateAccount(long confirmationCodeId, string code)
        {
            var confirmationCode = ConfirmationCodeRepository.GetById(confirmationCodeId);

            if (confirmationCode == null)
            {
                Throw.Critical(Errors.Security_ActivateAccount_InvalidActivationCode, "Invalid confirmation code id: {0}", confirmationCodeId);
            }

            if (confirmationCode.Code != code)
            {
                SetConfirmationCodeAsUsed(confirmationCode, ConfirmationCodeStatus.Failed);
                Throw.Critical(Errors.Security_ActivateAccount_InvalidActivationCode, "Invalid confirmation code: id={0}, code={1}", confirmationCodeId, code);
            }

            if (confirmationCode.Status != ConfirmationCodeStatus.AwaitingConfirmation)
            {
                SetConfirmationCodeAsUsed(confirmationCode, ConfirmationCodeStatus.Failed);
                Throw.Critical(Errors.Security_ActivateAccount_InvalidActivationCode, "Already confirmed confirmation code: id={0}, code={1}", confirmationCodeId, code);
            }

            if (confirmationCode.ExpireDate < DateTime.Now)
            {
                SetConfirmationCodeAsUsed(confirmationCode, ConfirmationCodeStatus.Expired);
                Throw.Critical(Errors.Security_ActivateAccount_InvalidActivationCode, "Confirmation code expired: id={0}, code={1}", confirmationCodeId, code);
            }

            if (confirmationCode.Reason != ConfirmationReason.NewUser &&
                confirmationCode.Reason != ConfirmationReason.AccountReactivation)
            {
                SetConfirmationCodeAsUsed(confirmationCode, ConfirmationCodeStatus.Failed);
                Throw.Critical(Errors.Security_ActivateAccount_InvalidActivationCode, "Invalid confirmation code reason: id={0}, code={1}, reason: {2}", confirmationCodeId, code, confirmationCode.Reason);
            }

            var user = UserRepository.GetById(confirmationCode.UserId);

            if (user == null)
            {
                SetConfirmationCodeAsUsed(confirmationCode, ConfirmationCodeStatus.Failed);
                Throw.Critical(Errors.Security_ActivateAccount_InvalidActivationCode, "User not found for confirmation code: id={0}, code={1}, userId: {2}", confirmationCodeId, code, confirmationCode.UserId);
            }

            if (user.Status != UserStatus.AwaitingActivation)
            {
                SetConfirmationCodeAsUsed(confirmationCode, ConfirmationCodeStatus.Failed);
                Throw.Critical(Errors.Security_ActivateAccount_InvalidActivationCode, "Invalid User status for confirmation code: id={0}, code={1}, userId: {2}, usersTatus: {3}", confirmationCodeId, code, confirmationCode.UserId, user.Status);
            }

            SetConfirmationCodeAsUsed(confirmationCode, ConfirmationCodeStatus.Confirmed);

            user.Status = UserStatus.Active;
            UserRepository.Save(user);
        }

        public virtual User ValidateAuthToken(long tokenId, string guid)
        {
            var token = TokenRepository.Get(tokenId);

            if (token == null)
            {
                Throw.Critical(Errors.Security_InvalidAuthToken, "No token found with the id {0}", tokenId);
            }

            if (token.Guid != guid)
            {
                Throw.Critical(Errors.Security_InvalidAuthToken, "Invalid guid ({0}) for token id {1}", guid, tokenId);
            }

            if (token.ExpireDate < DateTime.Now)
            {
                TokenRepository.Delete(token);
                Throw.Critical(Errors.Security_InvalidAuthToken, "Expired token");
            }

            var user = UserRepository.GetById(token.UserId);

            if (user.Status != UserStatus.Active)
            {
                TokenRepository.Delete(token);
                Throw.Critical(Errors.Security_InvalidAuthToken, "User not active");
            }

            token.UseDate = DateTime.Now;
            token.ExpireDate = DateTime.Now.AddDays(15);
            TokenRepository.Save(token);

            return user;
        }

        private void SetConfirmationCodeAsUsed(ConfirmationCode confirmationCode, ConfirmationCodeStatus status)
        {
            confirmationCode.Status = status;
            ConfirmationCodeRepository.Save(confirmationCode);
        }
    }
}
