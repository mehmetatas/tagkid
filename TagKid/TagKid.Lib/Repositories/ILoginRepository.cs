using Taga.Core.Repository;
using TagKid.Lib.Models.Entities;

namespace TagKid.Lib.Repositories
{
    public interface ILoginRepository : ITagKidRepository
    {
        IPage<Login> GetLogins(string username, string email, bool onlyFailed, int pageIndex, int pageSize);

        Login GetLastSuccessfulLogin(long userId);

        void Save(Login login);
    }
}