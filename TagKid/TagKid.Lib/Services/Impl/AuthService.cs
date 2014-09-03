using Taga.Core.DynamicProxy;
using Taga.Core.Repository;
using TagKid.Lib.Entities;
using TagKid.Lib.Exceptions;
using TagKid.Lib.Utils;

namespace TagKid.Lib.Services.Impl
{
    [Intercept]
    public class AuthService : IAuthService
    {
        private readonly IRepository _repo;
        private readonly IUnitOfWork _uow;

        public AuthService()
        {
            _uow = Db.UnitOfWork();
            _repo = Db.Repository();
        }

        public virtual void SignUp(User user)
        {
            Check(user);

            var existing =
                _repo.Get<User>(Db.Sql("select * from users where username = @0", user.Username));

            if (existing != null)
            {
                if (existing.Status == UserStatus.Active)
                    throw new UserException("User {0} already exists!", user.Username);
                if (existing.Status == UserStatus.Passive)
                    throw new UserException("User {0} is passive!", user.Username);
                if (existing.Status == UserStatus.AwaitingActivation)
                    throw new UserException("User {0} has not been actived yet!", user.Username);
                if (existing.Status == UserStatus.Banned)
                    throw new UserException("User {0} is banned!", user.Username);
            }

            existing =
                _repo.Get<User>(Db.Sql("select * from users where email = @0",
                    user.Email, UserStatus.Active, UserStatus.Passive, UserStatus.AwaitingActivation));

            if (existing != null)
            {
                if (existing.Status == UserStatus.Active)
                    throw new UserException("Email {0} already exists!", user.Email);
                if (existing.Status == UserStatus.Passive)
                    throw new UserException("Email {0} is passive!", user.Email);
                if(existing.Status == UserStatus.AwaitingActivation)
                    throw new UserException("Email {0} has not been actived yet!", user.Email);
                if (existing.Status == UserStatus.Banned)
                    throw new UserException("Email {0} is banned!", user.Email);
            }

            user.Status = UserStatus.Active;
            _repo.Save(user);

            _uow.Save();
        }

        public virtual User SignIn(string emailOrUsername, string password)
        {
            var user =
                _repo.Get<User>(Db.Sql("select * from users where email = @0 or username = @1", emailOrUsername, emailOrUsername));

            if (user == null || user.Password != password)
                throw new UserException("Login failed!");

            return user;
        }

        public virtual void SignUpWithFacebook(User user, string facebookAccessToken)
        {
            throw new System.NotImplementedException();
        }

        public virtual User SignInWithFacebook(string facebookId, string facebookAccessToken)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Dispose()
        {
            _uow.Dispose();
        }

        private static void Check(User user)
        {
            Validate.IsEmail(user.Email);
            Validate.StringLength("Username", user.Username, 30, 3);
            Validate.StringLength("Fullname", user.FullName, 30, 3);
            Validate.StringLength("Password", user.Password, 16, 6);
        }
    }
}
