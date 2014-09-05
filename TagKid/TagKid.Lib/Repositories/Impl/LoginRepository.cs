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
        public IPage<Login> GetByUserId(long userId, bool onlyFailed, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Login GetLastSuccessfulLogin(long userId)
        {
            var builder = Db.LinqQueryBuilder<Login>();
            
            builder.Where(l => l.UserId == userId && l.Result == LoginResult.Successful)
                .OrderBy(l => l.Id, true)
                .Page(0, 1);

            return Db.LinqRepository().Query(builder).Items.FirstOrDefault();
        }

        public void Save(Login login)
        {
            Db.LinqRepository().Save(login);
        }
    }
}
