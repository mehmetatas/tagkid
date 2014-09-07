using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Taga.Core.Repository;
using TagKid.Lib.Entities;
using TagKid.Lib.Utils;

namespace TagKid.Lib.Repositories.Impl
{
    public class LoginRepository : ILoginRepository
    {
        public IPage<Login> GetLogins(string username, string email, bool onlyFailed, int pageIndex, int pageSize)
        {
            var builder = Db.SqlBuilder();

            builder.SelectAllFrom("logins")
                .Where().Equals("username", username)
                .Or().Equals("email", email);

            if (onlyFailed)
                builder.And().Append("(")
                    .NotEquals("result", LoginResult.Successful)
                    .Or().NotEquals("result", LoginResult.SystemError)
                    .Append(")");

            return Db.SqlRepository().Page<Login>(pageIndex, pageSize, builder.Build());
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Login GetLastSuccessfulLogin(long userId)
        {
            var builder = Db.LinqQueryBuilder<Login>();

            builder.Where(l => l.UserId == userId && l.Result == LoginResult.Successful)
                .OrderBy(l => l.Id, true)
                .Page(1, 1);

            return Db.LinqRepository().Query(builder).Items.FirstOrDefault();
        }

        public void Save(Login login)
        {
            Db.LinqRepository().Save(login);
        }
    }
}
