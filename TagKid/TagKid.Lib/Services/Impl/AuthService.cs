using System;
using Taga.Core.DynamicProxy;
using TagKid.Lib.Models.DTO;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Exceptions;
using TagKid.Lib.Models.DTO.Messages;
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

                uow.Save();

                response = new SignInResponse
                {
                    AuthTokenId = authToken.Id,
                    AuthToken = authToken.Guid.ToString(),
                    RequestToken = Guid.NewGuid().ToString(),
                    User = new PublicUserModel(user),
                    UserId = user.Id
                };
            }

            return response;
        }

        public SignInResponse SignInWithToken(long tokenId, string guid)
        {
            var login = new Login { Date = DateTime.Now, Type = LoginType.Cookie };
            SignInResponse response;

            using (var uow = Db.UnitOfWork())
            {
                var token = Db.TokenRepository().GetById(tokenId);

                login.UserId = token == null ? 0 : token.UserId;

                if (token == null || token.Guid.ToString() != guid || token.Type != TokenType.Auth)
                {
                    login.Result = LoginResult.InvalidCookieToken;

                    Db.LoginRepository().Save(login);

                    throw new UserException("Invalid cookie token!");
                }

                if (DateTime.Now > token.Expires)
                {
                    login.Result = LoginResult.ExpiredCookieToken;
                    Db.LoginRepository().Save(login);

                    throw new UserException("Expired cookie token!");
                }

                var user = Db.UserRepository().GetById(token.UserId);

                if (user == null)
                {
                    login.Result = LoginResult.InvalidCookieToken;
                    Db.LoginRepository().Save(login);

                    throw new UserException("Invalid cookie token!");
                }

                ValidateStatus(user.Status, user.Username, "User", false);

                login.Result = LoginResult.Successful;
                Db.LoginRepository().Save(login);

                token.Expires = DateTime.Now.AddDays(7);

                Db.TokenRepository().Save(token);

                uow.Save();

                response = new SignInResponse
                {
                    AuthTokenId = token.Id,
                    AuthToken = token.Guid.ToString(),
                    RequestToken = Guid.NewGuid().ToString(),
                    User = new PublicUserModel(user),
                    UserId = user.Id
                };
            }

            return response;
        }

        private void ValidateStatus(UserStatus status, string fieldValue, string fieldName, bool isSignUp)
        {
            if (isSignUp && status == UserStatus.Active)
                throw new ValidationException("{0} {1} already exists!", fieldName, fieldValue);
            if (status == UserStatus.Passive)
                throw new ValidationException("{0} {1} is passive!", fieldName, fieldValue);
            if (status == UserStatus.AwaitingActivation)
                throw new ValidationException("{0} {1} has not been actived yet!", fieldName, fieldValue);
            if (status == UserStatus.Banned)
                throw new ValidationException("{0} {1} is banned!", fieldName, fieldValue);
        }
    }
}
