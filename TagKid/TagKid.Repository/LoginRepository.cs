using System.Linq;
using Taga.Core.Repository;
using TagKid.Core.Models.Database;
using TagKid.Core.Repository;

namespace TagKid.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IRepository _repository;

        public LoginRepository(IRepository repository)
        {
            _repository = repository;
        }

        public Login GetLastSuccessfulLogin(long userId)
        {
            return _repository.Select<Login>()
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