using System;
using Taga.Core.DynamicProxy;
using TagKid.Lib.Exceptions;
using TagKid.Lib.Models.DTO;
using TagKid.Lib.Models.DTO.Messages;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Utils;
using TagKid.Lib.Validation;

namespace TagKid.Lib.Services.Impl
{
    [Intercept]
    public class AuthService : IAuthService
    {
        public virtual SignUpResponse SignUp(SignUpRequest request)
        {
            using (var uow = Db.UnitOfWork())
            {
                Validator.Validate(request);

                var userRepo = Db.UserRepository();

                var existing = userRepo.GetByUsername(request.Username);
                if (existing != null)
                    ValidateStatus(existing.Status, request.Username, "User", true);

                existing = userRepo.GetByEmail(request.Email);
                if (existing != null)
                    ValidateStatus(existing.Status, request.Email, "Email", true);

                var user = new User
                {
                    Username = request.Username,
                    Password = Validate.EncryptPwd(request.Password),
                    Email = request.Email,
                    Fullname = request.Username,
                    FacebookId = request.FacebookId,
                    ProfileImageUrl = "",
                    JoinDate = DateTime.Now,
                    Type = UserType.User,
                    Status = UserStatus.Active
                };

                userRepo.Save(user);

                uow.Save();
            }

            return new SignUpResponse();
        }

        public virtual SignInResponse SignIn(SignInRequest request)
        {
            var login = new Login { Date = DateTime.Now };
            SignInResponse response;

            Validator.Validate(request);

            using (var uow = Db.UnitOfWork())
            {
                login.Type = LoginType.Email;
                login.FacebookId = String.Empty;

                login.Email = request.EmailOrUsername.Contains("@") ? request.EmailOrUsername : String.Empty;
                login.Username = login.Email == String.Empty ? request.EmailOrUsername : String.Empty;

                var user = request.EmailOrUsername.Contains("@")
                    ? Db.UserRepository().GetByEmail(login.Email)
                    : Db.UserRepository().GetByUsername(login.Username);

                if (user == null)
                {
                    login.Result = login.Email == String.Empty
                        ? LoginResult.InvalidUsername
                        : LoginResult.InvalidEmail;
                    Db.LoginRepository().Save(login);

                    throw new UserException("Invalid username/email or password");
                }

                if (user.Password != Validate.EncryptPwd(request.Password))
                {
                    login.Result = LoginResult.InvalidPassword;
                    Db.LoginRepository().Save(login);

                    throw new UserException("Invalid username/email or password");
                }

                ValidateStatus(user.Status, user.Username, "User", false);

                login.Result = LoginResult.Successful;
                Db.LoginRepository().Save(login);

                var authToken = new Token
                {
                    Expires = DateTime.Now.AddDays(7),
                    UserId = user.Id,
                    Guid = Guid.NewGuid(),
                    Type = TokenType.Auth
                };

                Db.TokenRepository().Save(authToken);

                var requestToken = new Token
                {
                    Expires = DateTime.Now.AddDays(1),
                    Guid = Guid.NewGuid(),
                    Type = TokenType.Request,
                    UserId = user.Id
                };
                Db.TokenRepository().Save(requestToken);

                uow.Save();

                response = new SignInResponse
                {
                    AuthTokenId = authToken.Id,
                    AuthToken = authToken.Guid.ToString(),
                    RequestToken = requestToken.Guid.ToString(),
                    RequestTokenId = requestToken.Id,
                    User = new PublicUserModel(user),
                    UserId = user.Id
                };
            }

            return response;
        }

        public virtual SignInResponse SignInWithToken(long tokenId, string guid)
        {
            var login = new Login { Date = DateTime.Now, Type = LoginType.Cookie };
            SignInResponse response;

            using (var uow = Db.UnitOfWork())
            {
                var authToken = Db.TokenRepository().GetById(tokenId);

                login.UserId = authToken == null ? 0 : authToken.UserId;

                if (authToken == null || authToken.Guid.ToString() != guid || authToken.Type != TokenType.Auth)
                {
                    login.Result = LoginResult.InvalidCookieToken;

                    Db.LoginRepository().Save(login);

                    throw new UserException("Invalid cookie token!");
                }

                if (DateTime.Now > authToken.Expires)
                {
                    login.Result = LoginResult.ExpiredCookieToken;
                    Db.LoginRepository().Save(login);

                    throw new UserException("Expired cookie token!");
                }

                var user = Db.UserRepository().GetById(authToken.UserId);

                if (user == null)
                {
                    login.Result = LoginResult.InvalidCookieToken;
                    Db.LoginRepository().Save(login);

                    throw new UserException("Invalid cookie token!");
                }

                ValidateStatus(user.Status, user.Username, "User", false);

                login.Result = LoginResult.Successful;
                Db.LoginRepository().Save(login);

                authToken.Expires = DateTime.Now.AddDays(7);

                Db.TokenRepository().Save(authToken);

                var requestToken = new Token
                {
                    Expires = DateTime.Now.AddDays(1),
                    Guid = Guid.NewGuid(),
                    Type = TokenType.Request,
                    UserId = user.Id
                };
                Db.TokenRepository().Save(requestToken);

                uow.Save();

                response = new SignInResponse
                {
                    AuthTokenId = authToken.Id,
                    AuthToken = authToken.Guid.ToString(),
                    RequestToken = requestToken.Guid.ToString(),
                    RequestTokenId = requestToken.Id,
                    User = new PublicUserModel(user),
                    UserId = user.Id
                };
            }

            return response;
        }

        public virtual void ValidateRequest(Request request)
        {
            if (request.Context.RequestToken.Id < 1 ||
                request.Context.RequestToken.Guid.Equals(Guid.Empty))
                throw new SecurityException("No request token!");

            if (request.Context.AuthToken.Id < 1 ||
                request.Context.AuthToken.Guid.Equals(Guid.Empty))
                throw new SecurityException("No auth token!");
            
            using (var uow = Db.UnitOfWork())
            {
                // Validate tokens
                var authToken = ValidateToken(request.Context.AuthToken);
                var requestToken = ValidateToken(request.Context.RequestToken);

                // Validate user
                var user = Db.UserRepository().GetById(authToken.UserId);
                if (user.Status != UserStatus.Active)
                    throw new SecurityException("User is not active!");

                // Update request token
                requestToken.UsedTime = DateTime.Now;
                requestToken.Expires = DateTime.Now;
                Db.TokenRepository().Save(requestToken);

                // Create new request token
                var newRequestToken = new Token
                {
                    Expires = DateTime.Now.AddDays(1),
                    Guid = Guid.NewGuid(),
                    Type = TokenType.Request,
                    UserId = user.Id
                };
                Db.TokenRepository().Save(newRequestToken);

                // Save Changes to db
                uow.Save();

                // Update request context
                request.Context.User = user;
                request.Context.AuthToken = authToken;
                request.Context.RequestToken = requestToken;
                request.Context.NewRequestToken = newRequestToken;
            }
        }

        private Token ValidateToken(Token token)
        {
            var dbToken = Db.TokenRepository().GetById(token.Id);

            if (dbToken == null)
                throw new SecurityException(token.Type + " token not found!");

            if (!dbToken.Guid.Equals(token.Guid))
                throw new SecurityException(token.Type + " token not match!");

            if (dbToken.Type != token.Type)
                throw new SecurityException(token.Type + " token type not match!");

            if (dbToken.Expires < DateTime.Now)
                throw new SecurityException(token.Type + " token expired!");

            return dbToken;
        }

        private void ValidateStatus(UserStatus status, string fieldValue, string fieldName, bool isSignUp)
        {
            if (isSignUp && status == UserStatus.Active)
                throw new ValidationException("{0} {1} already exists!", fieldName, fieldValue);
            if (status == UserStatus.Passive)
                throw new SecurityException("{0} {1} is passive!", fieldName, fieldValue);
            if (status == UserStatus.AwaitingActivation)
                throw new SecurityException("{0} {1} has not been actived yet!", fieldName, fieldValue);
            if (status == UserStatus.Banned)
                throw new SecurityException("{0} {1} is banned!", fieldName, fieldValue);
        }
    }
}
