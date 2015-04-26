using System.Linq;
using TagKid.Core.Models.Database;
using TagKid.Core.Repository;
using TagKid.Framework.Repository;

namespace TagKid.Core.Providers.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly IRepository _repo;

        public UserRepository(IRepository repo)
        {
            _repo = repo;
        }

        public void Save(User user)
        {
            _repo.Save(user);
        }

        public User GetByUsername(string username)
        {
            return _repo.Select<User>()
                .FirstOrDefault(user => user.Username == username);
        }

        public User GetByEmail(string email)
        {
            return _repo.Select<User>()
                .FirstOrDefault(user => user.Email == email);
        }
    }
}
