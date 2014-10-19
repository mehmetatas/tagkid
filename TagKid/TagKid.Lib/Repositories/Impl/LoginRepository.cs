using System.Linq;
using Taga.Core.Repository;
using TagKid.Lib.Models.Entities;

namespace TagKid.Lib.Repositories.Impl
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IRepository _repository;

        public LoginRepository(IRepository repository)
        {
            _repository = repository;
        }

        public IPage<Login> GetLogins(string username, string email, bool onlyFailed, int pageIndex, int pageSize)
        {
            var query = _repository.Query<Login>()
                .Where(l => l.Username == username || l.Email == email);

            if (onlyFailed)
            {
                query = query.Where(l => l.Result != LoginResult.Successful || l.Result != LoginResult.SystemError);
            }

            return query.Page(pageIndex, pageSize);
        }

        public Login GetLastSuccessfulLogin(long userId)
        {
            return _repository.Query<Login>()
                .Where(l => l.UserId == userId && l.Result == LoginResult.Successful)
                .OrderByDescending(l => l.Id)
                .FirstOrDefault();
        }

        public void Save(Login login)
        {
            _repository.Save(login);
        }
    }
}
