using Taga.Core.Repository;
using TagKid.Lib.Entities;

namespace TagKid.Lib.Repositories
{
    public interface ILoginRepository
    {
        IPage<Login> GetByUserId(long userId, bool onlyFailed, int pageIndex, int pageSize);

        Login GetLastSuccessfulLogin(long userId);

        void Save(Login login);
    }
}
