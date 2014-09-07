using Taga.Core.Repository;
using Taga.Core.Repository.Sql;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Utils;

namespace TagKid.Lib.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        public User GetById(long id)
        {
            return Db.SqlRepository().FirstOrDefault<User>(u => u.Id, id);
        }

        public User GetByEmail(string email)
        {
            return Db.SqlRepository().FirstOrDefault<User>(u => u.Email, email);
        }

        public User GetByUsername(string username)
        {
            return Db.SqlRepository().FirstOrDefault<User>(u => u.Username, username);
        }

        public void Save(User user)
        {
            Db.SqlRepository().Save(user);
        }

        public IPage<User> Search(string usernameOrFullname, int pageIndex, int pageSize)
        {
            return Db.SqlRepository().ExecuteQuery<User>(Db.SqlBuilder()
                .SelectAllFrom<User>()
                .Where("username").Contains(usernameOrFullname)
                .Or("fullname").Contains(usernameOrFullname)
                .Build(),
                pageIndex, pageSize);
        }
    }
}
