using Taga.Core.Repository;
using Taga.Core.Repository.Linq;
using TagKid.Lib.Entities;
using TagKid.Lib.Utils;

namespace TagKid.Lib.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        public User GetById(long id)
        {
            return Db.LinqRepository().FirstOrDefault<User>(u => u.Id == id);
        }

        public User GetByEmail(string email)
        {
            return Db.LinqRepository().FirstOrDefault<User>(u => u.Email == email);
        }

        public User GetByUsername(string username)
        {
            return Db.LinqRepository().FirstOrDefault<User>(u => u.Username == username);
        }

        public void Save(User user)
        {
            Db.LinqRepository().Save(user);
        }

        public IPage<User> Search(string usernameOrFullname, int pageIndex, int pageSize)
        {
            return Db.LinqRepository().Query<User>(
                u => u.Username.Contains(usernameOrFullname) || u.FullName.Contains(usernameOrFullname),
                pageIndex, pageSize);
        }
    }
}
