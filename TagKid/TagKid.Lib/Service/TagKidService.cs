using Taga.Core.DynamicProxy;
using TagKid.Lib.Entities;
using TagKid.Lib.Exceptions;
using TagKid.Lib.Repository;
using TagKid.Lib.Utils;

namespace TagKid.Lib.Service
{
    [Intercept]
    public class TagKidService
    {
        public virtual void RegisterUser(User user)
        {
            Check(user);

            using (var db = new Db(false))
            {
                var existing =
                    db.Get<User>(Db.Sql("select * from users where username = @0", user.Username));

                if (existing != null)
                {
                    if (existing.Status == UserStatus.Active)
                        throw new UserException("User {0} already exists!", user.Username);
                    if (existing.Status == UserStatus.Passive)
                        throw new UserException("User {0} is passive!", user.Username);
                    if (existing.Status == UserStatus.WaitingActivation)
                        throw new UserException("User {0} has not been actived yet!", user.Username);
                    if (existing.Status == UserStatus.Banned)
                        throw new UserException("User {0} is banned!", user.Username);
                }
                
                existing =
                    db.Get<User>(Db.Sql("select * from users where email = @0",
                        user.Email, UserStatus.Active, UserStatus.Passive, UserStatus.WaitingActivation));

                if (existing != null)
                {
                    if (existing.Status == UserStatus.Active)
                        throw new UserException("Email {0} already exists!", user.Email);
                    if (existing.Status == UserStatus.Passive)
                        throw new UserException("Email {0} is passive!", user.Email);
                    if (existing.Status == UserStatus.WaitingActivation)
                        throw new UserException("Email {0} has not been actived yet!", user.Email);
                    if (existing.Status == UserStatus.Banned)
                        throw new UserException("Email {0} is banned!", user.Email);
                }

                user.Status = UserStatus.Active;
                db.Save(user);
            }
        }

        public virtual void LoginUser(string emailOrUsername, string password)
        {
            using (var db = new Db(false))
            {
                var user =
                    db.Get<User>(Db.Sql("select * from users where email = @0 or username = @1", emailOrUsername, emailOrUsername));

                if (user == null || user.Password != password)
                    throw new UserException("Login failed!");
            }
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
