using Taga.Core.Repository;
using TagKid.Core.Models.Database;

namespace TagKid.Core.Repositories
{
    public interface IUserRepository : ITagKidRepository
    {
        User GetById(long id);

        User GetByEmail(string email);

        User GetByUsername(string username);

        void Save(User user);

        IPage<User> Search(string usernameOrFullname, int pageIndex, int pageSize);
    }
}