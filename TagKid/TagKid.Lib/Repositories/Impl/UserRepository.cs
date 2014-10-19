using System.Linq;
using Taga.Core.Repository;
using TagKid.Lib.Models.Entities;

namespace TagKid.Lib.Repositories.Impl
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
            return _repository.Query<User>()
                .FirstOrDefault(u => u.Id == id);
        }

        public User GetByEmail(string email)
        {
            return _repository.Query<User>()
                .FirstOrDefault(u => u.Email == email);
        }

        public User GetByUsername(string username)
        {
            return _repository.Query<User>()
                .FirstOrDefault(u => u.Username == username);
        }

        public void Save(User user)
        {
            _repository.Save(user);
        }

        public IPage<User> Search(string usernameOrFullname, int pageIndex, int pageSize)
        {
            return _repository.Query<User>()
                .Where(u => u.Username.Contains(usernameOrFullname) || u.Fullname.Contains(usernameOrFullname))
                .OrderBy(u => u.Username)
                .Page(pageIndex, pageSize);
        }
    }
}