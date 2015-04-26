using TagKid.Core.Models.Database;

namespace TagKid.Core.Repository
{
    public interface IUserRepository
    {
        void Save(User user);
        User GetByUsername(string username);
        User GetByEmail(string email);
    }
}
