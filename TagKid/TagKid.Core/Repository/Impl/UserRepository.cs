using DummyOrm.Db;
using TagKid.Core.Models.Database;
using TagKid.Framework.UnitOfWork;

namespace TagKid.Core.Repository.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly IRepository _db;

        public UserRepository(IRepository db)
        {
            _db = db;
        }

        public void Save(User user)
        {
            if (user.Id > 0)
            {
                _db.Update(user);
            }
            else
            {
                _db.Insert(user);
            }
        }

        public User GetByUsername(string username)
        {
            return _db.Select<User>()
                .FirstOrDefault(user => user.Username == username);
        }

        public User GetByEmail(string email)
        {
            return _db.Select<User>()
                .FirstOrDefault(user => user.Email == email);
        }
    }
}
