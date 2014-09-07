using Taga.Core.Repository;
using Taga.Core.Repository.Sql;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Utils;

namespace TagKid.Lib.Repositories.Impl
{
    public class LoginRepository : ILoginRepository
    {
        public IPage<Login> GetLogins(string username, string email, bool onlyFailed, int pageIndex, int pageSize)
        {
            var sqlBuilder = Db.SqlBuilder();

            sqlBuilder.SelectAllFrom("logins")
                .Where().Equals("username", username)
                .Or().Equals("email", email);

            if (onlyFailed)
                sqlBuilder.And().Append("(")
                    .NotEquals("result", LoginResult.Successful)
                    .Or().NotEquals("result", LoginResult.SystemError)
                    .Append(")");

            return Db.SqlRepository().ExecuteQuery<Login>(sqlBuilder.Build(), pageIndex, pageSize);
        }

        public Login GetLastSuccessfulLogin(long userId)
        {
            var builder = Db.SqlBuilder();

            builder.SelectAllFrom<Login>()
                .Where("user_id").EqualsParam(userId)
                .And("result").EqualsParam(LoginResult.Successful)
                .OrderBy("id", true);

            return Db.SqlRepository().FirstOrDefault<Login>(builder.Build());
        }

        public void Save(Login login)
        {
            Db.SqlRepository().Save(login);
        }
    }
}
