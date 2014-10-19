using System;
using Taga.Core.DynamicProxy;
using TagKid.Lib.Database;
using TagKid.Lib.Exceptions;
using TagKid.Lib.Models.DTO.Messages;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Repositories;
using TagKid.Lib.Utils;
using TagKid.Lib.Validation;

namespace TagKid.Lib.Services.Impl
{
    [Intercept]
    public class AuthService : IAuthService
    {
        public virtual SignUpResponse SignUp(SignUpRequest request)
        {
            Validator.Validate(request);

            using (var db = new TagKidDb())
            {
                var userRepo = db.GetRepository<IUserRepository>();

                var reqUser = request.User;

                var existing = userRepo.GetByUsername(reqUser.Username);
                if (existing != null)
                    ValidateStatus(existing.Status, request.User.Username, "User", true);

                existing = userRepo.GetByEmail(reqUser.Email);
                if (existing != null)
                    ValidateStatus(existing.Status, reqUser.Email, "Email", true);

                var user = reqUser.ToUser();

                user.Password = Util.EncryptPwd(reqUser.Password);
                user.JoinDate = DateTime.Now;
                user.Type = UserType.User;
                user.Status = UserStatus.Active;

                userRepo.Save(user);

                db.Save();
            }

            return new SignUpResponse();
        }

        public virtual SignInResponse SignIn(SignInRequest request)
        {
            var login = new Login {Date = DateTime.Now};
            SignInResponse response;

            Validator.Validate(request);

            using (var db = new TagKidDb())
            {
                login.Type = LoginType.Email;
                login.FacebookId = String.Empty;

                login.Email = request.EmailOrUsername.Contains("@") ? request.EmailOrUsername : String.Empty;
                login.Username = login.Email == String.Empty ? request.EmailOrUsername : String.Empty;

                var userRepo = db.GetRepository<IUserRepository>();
                var loginRepo = db.GetRepository<ILoginRepository>();
                var tokenRepo = db.GetRepository<ITokenRepository>();

                var user = request.EmailOrUsername.Contains("@")
                    ? userRepo.GetByEmail(login.Email)
                    : userRepo.GetByUsername(login.Username);

                if (user == null)
                {
                    login.Result = login.Email == String.Empty
                        ? LoginResult.InvalidUsername
                        : LoginResult.InvalidEmail;
                    loginRepo.Save(login);

                    throw new UserException("Invalid username/email or password");
                }

                if (user.Password != Util.EncryptPwd(request.Password))
                {
                    login.Result = LoginResult.InvalidPassword;
                    loginRepo.Save(login);

                    throw new UserException("Invalid username/email or password");
                }

                ValidateStatus(user.Status, user.Username, "User", false);

                login.Result = LoginResult.Successful;
                loginRepo.Save(login);

                var authToken = new Token
                {
                    Expires = DateTime.Now.AddDays(7),
                    UserId = user.Id,
                    Guid = Guid.NewGuid(),
                    Type = TokenType.Auth
                };

                tokenRepo.Save(authToken);

                var requestToken = new Token
                {
                    Expires = DateTime.Now.AddDays(1),
                    Guid = Guid.NewGuid(),
                    Type = TokenType.Request,
                    UserId = user.Id
                };
                tokenRepo.Save(requestToken);

                db.Save();

                response = new SignInResponse
                {
                    AuthTokenId = authToken.Id,
                    AuthToken = authToken.Guid.ToString(),
                    RequestToken = requestToken.Guid.ToString(),
                    RequestTokenId = requestToken.Id,
                    User = user.ToPublicUserModel(),
                    UserId = user.Id
                };
            }

            return response;
        }

        public virtual SignInResponse SignInWithToken(long tokenId, string guid)
        {
            var login = new Login {Date = DateTime.Now, Type = LoginType.Cookie};
            SignInResponse response;

            using (var db = new TagKidDb())
            {
                var userRepo = db.GetRepository<IUserRepository>();
                var loginRepo = db.GetRepository<ILoginRepository>();
                var tokenRepo = db.GetRepository<ITokenRepository>();

                var authToken = tokenRepo.GetById(tokenId);

                login.UserId = authToken == null ? 0 : authToken.UserId;

                if (authToken == null || authToken.Guid.ToString() != guid || authToken.Type != TokenType.Auth)
                {
                    login.Result = LoginResult.InvalidCookieToken;

                    loginRepo.Save(login);

                    throw new UserException("Invalid cookie token!");
                }

                if (DateTime.Now > authToken.Expires)
                {
                    login.Result = LoginResult.ExpiredCookieToken;
                    loginRepo.Save(login);

                    throw new UserException("Expired cookie token!");
                }

                var user = userRepo.GetById(authToken.UserId);

                if (user == null)
                {
                    login.Result = LoginResult.InvalidCookieToken;
                    loginRepo.Save(login);

                    throw new UserException("Invalid cookie token!");
                }

                ValidateStatus(user.Status, user.Username, "User", false);

                login.Result = LoginResult.Successful;
                loginRepo.Save(login);

                authToken.Expires = DateTime.Now.AddDays(7);

                tokenRepo.Save(authToken);

                var requestToken = new Token
                {
                    Expires = DateTime.Now.AddDays(1),
                    Guid = Guid.NewGuid(),
                    Type = TokenType.Request,
                    UserId = user.Id
                };
                tokenRepo.Save(requestToken);

                db.Save();

                response = new SignInResponse
                {
                    AuthTokenId = authToken.Id,
                    AuthToken = authToken.Guid.ToString(),
                    RequestToken = requestToken.Guid.ToString(),
                    RequestTokenId = requestToken.Id,
                    User = user.ToPublicUserModel(),
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

            using (var db = new TagKidDb())
            {
                var userRepo = db.GetRepository<IUserRepository>();
                var tokenRepo = db.GetRepository<ITokenRepository>();

                // Validate tokens
                var authToken = ValidateToken(
                    request.Context.AuthToken,
                    tokenRepo.GetById(request.Context.AuthToken.Id));

                var requestToken = ValidateToken(
                    request.Context.RequestToken,
                    tokenRepo.GetById(request.Context.RequestToken.Id));

                // Validate user
                var user = userRepo.GetById(authToken.UserId);
                if (user.Status != UserStatus.Active)
                    throw new SecurityException("User is not active!");

                // Update request token
                requestToken.UsedTime = DateTime.Now;
                requestToken.Expires = DateTime.Now;
                tokenRepo.Save(requestToken);

                // Create new request token
                var newRequestToken = new Token
                {
                    Expires = DateTime.Now.AddDays(1),
                    Guid = Guid.NewGuid(),
                    Type = TokenType.Request,
                    UserId = user.Id
                };
                tokenRepo.Save(newRequestToken);

                // Save Changes to db
                db.Save();

                // Update request context
                request.Context.User = user;
                request.Context.AuthToken = authToken;
                request.Context.RequestToken = requestToken;
                request.Context.NewRequestToken = newRequestToken;
            }
        }

        private static Token ValidateToken(Token tokenFromClient, Token dbToken)
        {
            if (dbToken == null)
                throw new SecurityException(tokenFromClient.Type + " token not found!");

            if (!dbToken.Guid.Equals(tokenFromClient.Guid))
                throw new SecurityException(tokenFromClient.Type + " token not match!");

            if (dbToken.Type != tokenFromClient.Type)
                throw new SecurityException(tokenFromClient.Type + " token type not match!");

            if (dbToken.Expires < DateTime.Now)
                throw new SecurityException(tokenFromClient.Type + " token expired!");

            return dbToken;
        }

        private static void ValidateStatus(UserStatus status, string fieldValue, string fieldName, bool isSignUp)
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