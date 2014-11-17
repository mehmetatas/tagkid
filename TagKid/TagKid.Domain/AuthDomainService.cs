using System;
using Taga.Core.DynamicProxy;
using TagKid.Core.Domain;
using TagKid.Core.Exceptions;
using TagKid.Core.Models.Database;
using TagKid.Core.Repository;
using TagKid.Core.Utils;

namespace TagKid.Domain
{
    [Intercept]
    public class AuthDomainService : IAuthDomainService
    {
        private readonly IRepositoryProvider _prov;

        public AuthDomainService(IRepositoryProvider prov)
        {
            _prov = prov;
        }

        private IUserRepository UserRepository
        {
            get { return _prov.GetRepository<IUserRepository>(); }
        }

        private ITokenRepository TokenRepository
        {
            get { return _prov.GetRepository<ITokenRepository>(); }
        }

        public virtual void SignUpWithEmail(string email, string username, string password, string fullname)
        {
            var user = UserRepository.GetByEmail(email);
            if (user != null)
            {
                throw new Exception("Email already exists!");
            }

            user = UserRepository.GetByUsername(username);
            if (user != null)
            {
                throw new Exception("Username already exists!");
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
                E.T(ErrorCodes.Security_InvalidAuthToken, "No token found with the id {0}", tokenId);
            }

            if (token.Guid != guid)
            {
                E.T(ErrorCodes.Security_InvalidAuthToken, "Invalid guid ({0}) for token id {1}", guid, tokenId);
            }

            if (token.ExpireDate < DateTime.Now)
            {
                TokenRepository.Delete(token);
                E.T(ErrorCodes.Security_InvalidAuthToken, "Expired token");
            }

            var user = UserRepository.GetById(token.UserId);

            if (user.Status != UserStatus.Active)
            {
                TokenRepository.Delete(token);
                E.T(ErrorCodes.Security_InvalidAuthToken, "User not active");
            }

            token.UseDate = DateTime.Now;
            token.ExpireDate = DateTime.Now.AddDays(15);
            TokenRepository.Save(token);

            return user;
        }

        public virtual void ValidateRequestToken(long tokenId, string guid)
        {
            
        }

        public virtual void IssueNewRequestToken()
        {
            
        }
    }
}
