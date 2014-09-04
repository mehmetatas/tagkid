using System;
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
            var repo = Db.LinqRepository();
            return repo.FirstOrDefault<User>(u => u.Id == id);
        }

        public User GetByEmail(string email)
        {
            var repo = Db.LinqRepository();
            return repo.FirstOrDefault<User>(u => u.Email == email);
        }

        public User GetByUsername(string username)
        {
            var repo = Db.LinqRepository();
            return repo.FirstOrDefault<User>(u => u.Username == username);
        }

        public void Save(User user)
        {
            var repo = Db.LinqRepository();
            repo.Save(user);
        }

        public IPage<User> Search(string usernameOrFullname, int pageIndex, int pageSize)
        {
            var repo = Db.LinqRepository();
            return repo.Query<User>(
                u => u.Username.Contains(usernameOrFullname) || u.FullName.Contains(usernameOrFullname),
                pageIndex, pageSize);
        }
    }
}
