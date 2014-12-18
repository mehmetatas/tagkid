using System;
using System.Collections;
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
        private readonly static Hashtable AuthTokens = Hashtable.Synchronized(new Hashtable());

        private static IUserRepository UserRepository
        {
            get { return ServiceProvider.Provider.GetOrCreate<IUserRepository>(); }
        }

        private static ITokenRepository TokenRepository
        {
            get { return ServiceProvider.Provider.GetOrCreate<ITokenRepository>(); }
        }

        private static IConfirmationCodeRepository ConfirmationCodeRepository
        {
            get { return ServiceProvider.Provider.GetOrCreate<IConfirmationCodeRepository>(); }
        }

        private static IMailService MailService
        {
            get { return ServiceProvider.Provider.GetOrCreate<IMailService>(); }
        }

        public virtual void SignUpWithEmail(string email, string username, string password, string fullname)
        {
            var user = UserRepository.GetByEmail(email);
            if (user != null)
            {
                throw Errors.V_EmailAlreadyExists.ToException();
            }

            user = UserRepository.GetByUsername(username);
            if (user != null)
            {
                throw Errors.V_UsernameAlreadyExists.ToException();
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
                throw Errors.S_InvalidUsernameOrPassword.ToException("Invalid email or username: {0}", emailOrUsername);
            }

            if (user.Password != Util.EncryptPwd(password))
            {
                throw Errors.S_InvalidUsernameOrPassword.ToException("Invalid password for: {0} {1}", user.Username,
                    user.Id);
            }

            if (user.Status != UserStatus.Active)
            {
                throw Errors.S_InvalidUsernameOrPassword.ToException("User not active: {0} {1}", user.Username, user.Id);
            }

            IssueNewAuthToken(user);

            return user;
        }

        public virtual User SignInWithFacebook(string facebookId, string facebookAuthToken)
        {
            throw new NotImplementedException();
        }

        public virtual User SignInWithToken(long tokenId, Guid guid)
        {
            var token = TokenRepository.Get(tokenId);

            if (token == null)
            {
                throw Errors.S_InvalidAuthToken.ToException("No token found with the id {0}", tokenId);
            }

            if (token.Guid != guid)
            {
                throw Errors.S_InvalidAuthToken.ToException("Invalid guid ({0}) for token id {1}", guid, tokenId);
            }

            if (token.ExpireDate < DateTime.Now)
            {
                TokenRepository.Delete(token);
                throw Errors.S_InvalidAuthToken.ToException("Expired token");
            }

            var user = token.User ?? UserRepository.GetById(token.UserId);

            if (user == null)
            {
                TokenRepository.Delete(token);
                throw Errors.S_InvalidAuthToken.ToException("No user found for token");
            }

            if (user.Status != UserStatus.Active)
            {
                TokenRepository.Delete(token);
                throw Errors.S_InvalidAuthToken.ToException("User not active");
            }
            
            token.UseDate = DateTime.Now;
            token.ExpireDate = DateTime.Now.AddDays(15);
            TokenRepository.Save(token);

            SetAuthToken(token, user);

            return user;
        }

        public virtual void SetupRequestContext(long tokenId, Guid guid)
        {
            var token = GetAuthToken(guid);

            if (token == null)
            {
                SignInWithToken(tokenId, guid);
            }
            else
            {
                RequestContext.Current.AuthToken = token;
            }
        }

        public virtual void ResetPassword(string email)
        {

        }

        public virtual void RequestReactivation(string email)
        {

        }

        public virtual User ActivateAccount(long confirmationCodeId, string code)
        {
            var confirmationCode = ConfirmationCodeRepository.GetById(confirmationCodeId);

            if (confirmationCode == null)
            {
                throw Errors.S_InvalidActivationCode.ToException("Invalid confirmation code id: {0}", confirmationCodeId);
            }

            if (confirmationCode.Code != code)
            {
                SetConfirmationCodeAsUsed(confirmationCode, ConfirmationCodeStatus.Failed);
                throw Errors.S_InvalidActivationCode.ToException("Invalid confirmation code: id={0}, code={1}",
                    confirmationCodeId, code);
            }

            if (confirmationCode.Status != ConfirmationCodeStatus.AwaitingConfirmation)
            {
                SetConfirmationCodeAsUsed(confirmationCode, ConfirmationCodeStatus.Failed);
                throw Errors.S_InvalidActivationCode.ToException(
                    "Already confirmed confirmation code: id={0}, code={1}", confirmationCodeId, code);
            }

            if (confirmationCode.ExpireDate < DateTime.Now)
            {
                SetConfirmationCodeAsUsed(confirmationCode, ConfirmationCodeStatus.Expired);
                throw Errors.S_InvalidActivationCode.ToException("Confirmation code expired: id={0}, code={1}",
                    confirmationCodeId, code);
            }

            if (confirmationCode.Reason != ConfirmationReason.NewUser &&
                confirmationCode.Reason != ConfirmationReason.AccountReactivation)
            {
                SetConfirmationCodeAsUsed(confirmationCode, ConfirmationCodeStatus.Failed);
                throw Errors.S_InvalidActivationCode.ToException(
                    "Invalid confirmation code reason: id={0}, code={1}, reason: {2}", confirmationCodeId, code,
                    confirmationCode.Reason);
            }

            var user = UserRepository.GetById(confirmationCode.UserId);

            if (user == null)
            {
                SetConfirmationCodeAsUsed(confirmationCode, ConfirmationCodeStatus.Failed);
                throw Errors.S_InvalidActivationCode.ToException(
                    "User not found for confirmation code: id={0}, code={1}, userId: {2}",
                    confirmationCodeId, code, confirmationCode.UserId);
            }

            if (user.Status != UserStatus.AwaitingActivation)
            {
                SetConfirmationCodeAsUsed(confirmationCode, ConfirmationCodeStatus.Failed);
                throw Errors.S_InvalidActivationCode.ToException(
                    "Invalid User status for confirmation code: id={0}, code={1}, userId: {2}, usersTatus: {3}",
                    confirmationCodeId, code, confirmationCode.UserId, user.Status);
            }

            SetConfirmationCodeAsUsed(confirmationCode, ConfirmationCodeStatus.Confirmed);

            user.Status = UserStatus.Active;
            UserRepository.Save(user);

            IssueNewAuthToken(user);

            return user;
        }

        public virtual void SignOut()
        {
            RemoveAuthToken(RequestContext.Current.AuthToken.Guid);
            TokenRepository.Delete(RequestContext.Current.AuthToken);
            RequestContext.Current.AuthToken = null;
        }

        private void SetConfirmationCodeAsUsed(ConfirmationCode confirmationCode, ConfirmationCodeStatus status)
        {
            confirmationCode.Status = status;
            ConfirmationCodeRepository.Save(confirmationCode);
        }

        private void IssueNewAuthToken(User user)
        {
            var authToken = new Token
            {
                Guid = Util.GenerateGuid(),
                Type = TokenType.Auth,
                ExpireDate = DateTime.Now.AddDays(15),
                UserId = user.Id
            };

            TokenRepository.DeleteTokensOfUser(user.Id);
            TokenRepository.Save(authToken);

            SetAuthToken(authToken, user);
        }

        private void SetAuthToken(Token token, User user)
        {
            token.UserId = user.Id;
            token.User = user;
            AuthTokens.Add(token.Guid, token);
            RequestContext.Current.AuthToken = token;
        }

        private void RemoveAuthToken(Guid guid)
        {
            if (AuthTokens.ContainsKey(guid))
            {
                AuthTokens.Remove(guid);
            }
        }

        private Token GetAuthToken(Guid guid)
        {
            if (AuthTokens.ContainsKey(guid))
            {
                return (Token)AuthTokens[guid];
            }
            return null;
        }
    }
}
