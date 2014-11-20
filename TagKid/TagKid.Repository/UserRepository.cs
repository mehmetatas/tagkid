using System.Linq;
using Taga.Core.Repository;
using TagKid.Core.Models.Database;
using TagKid.Core.Repository;

namespace TagKid.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IRepository _repository;

        public UserRepository(IRepository repository)
        {
            _repository = repository;
        }

        public User GetById(long id)
        {
            return _repository.Select<User>()
                .FirstOrDefault(u => u.Id == id);
        }

        public User GetByEmail(string email)
        {
            return _repository.Select<User>()
                .FirstOrDefault(u => u.Email == email);
        }

        public User GetByUsername(string username)
        {
            return _repository.Select<User>()
                .FirstOrDefault(u => u.Username == username);
        }

        public void Save(User user)
        {
            _repository.Save(user);
        }

        public IPage<User> Search(string usernameOrFullname, int pageIndex, int pageSize)
        {
            return _repository.Select<User>()
                .Where(u => (u.Username.Contains(usernameOrFullname) || u.Fullname.Contains(usernameOrFullname)) && u.Status == UserStatus.Active)
                .OrderBy(u => u.Username)
                .Page(pageIndex, pageSize);
        }

        public void Flush()
        {
            _repository.Flush();
        }
    }
}