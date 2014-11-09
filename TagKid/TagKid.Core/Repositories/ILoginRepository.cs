using Taga.Core.Repository;
using TagKid.Core.Models.Database;

namespace TagKid.Core.Repositories
{
    public interface ILoginRepository : ITagKidRepository
    {
        Login GetLastSuccessfulLogin(long userId);

        void Save(Login login);
    }
}