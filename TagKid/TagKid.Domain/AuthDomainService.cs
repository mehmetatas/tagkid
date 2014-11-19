using System;
using Taga.Core.DynamicProxy;
using Taga.Core.IoC;
using TagKid.Core.Domain;
using TagKid.Core.Exceptions;
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

        public virtual void SignUpWithEmail(string email, string username, string password, string fullname)
        {
            var user = UserRepository.GetByEmail(email);
            if (user != null)
            {
                E.x(Errors.Validation_EmailAlreadyExists);
            }

            user = UserRepository.GetByUsername(username);
            if (user != null)
            {
                E.x(Errors.Validation_UsernameAlreadyExists);
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
        }

        public virtual void SignUpWithFacebook(string facebookId, string facebookAuthToken)
        {
            
        }

        public virtual void SignInWithPassword(string emailOrUsername, string password)
        {
            
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

        public virtual void ActivateAccount(long tokenId, string guid)
        {
            
        }

        public virtual User ValidateAuthToken(long tokenId, string guid)
        {
            var token = TokenRepository.Get(tokenId);

            if (token == null)
            {
                E.x(Errors.Security_InvalidAuthToken, "No token found with the id {0}", tokenId);
            }

            if (token.Guid != guid)
            {
                E.x(Errors.Security_InvalidAuthToken, "Invalid guid ({0}) for token id {1}", guid, tokenId);
            }

            if (token.ExpireDate < DateTime.Now)
            {
                TokenRepository.Delete(token);
                E.x(Errors.Security_InvalidAuthToken, "Expired token");
            }

            var user = UserRepository.GetById(token.UserId);

            if (user.Status != UserStatus.Active)
            {
                TokenRepository.Delete(token);
                E.x(Errors.Security_InvalidAuthToken, "User not active");
            }

            token.UseDate = DateTime.Now;
            token.ExpireDate = DateTime.Now.AddDays(15);
            TokenRepository.Save(token);

            return user;
        }
    }
}
