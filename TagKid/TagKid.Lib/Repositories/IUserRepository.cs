using Taga.Core.Repository;
using TagKid.Lib.Entities;

namespace TagKid.Lib.Repositories
{
    public interface IUserRepository
    {
        User GetById(long id);

        User GetByEmail(string email);

        User GetByUsername(string username);

        void Save(User user);

        IPage<User> Search(string usernameOrFullname, int pageIndex, int pageSize);
    }
}
