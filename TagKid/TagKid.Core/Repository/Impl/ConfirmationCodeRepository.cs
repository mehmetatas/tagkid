using DummyOrm.Db;
using TagKid.Core.Models.Database;
using TagKid.Framework.UnitOfWork;

namespace TagKid.Core.Repository.Impl
{
    public class ConfirmationCodeRepository : IConfirmationCodeRepository
    {
        private readonly IRepository _repo;

        public ConfirmationCodeRepository(IRepository repo)
        {
            _repo = repo;
        }

        public ConfirmationCode GetForActivation(long id)
        {
            return _repo.Select<ConfirmationCode>()
                .Join(c => c.User)
                .FirstOrDefault(c => c.Id == id);
        }

        public void Save(ConfirmationCode confCode)
        {
            if (confCode.Id > 0)
            {
                _repo.Update(confCode);
            }
            else
            {
                _repo.Insert(confCode);
            }
        }
    }
}
