using TagKid.Core.Models.Database;

namespace TagKid.Core.Repository
{
    public interface ILoginRepository : ITagKidRepository
    {
        Login GetLastSuccessfulLogin(long userId);

        void Save(Login login);
    }
}