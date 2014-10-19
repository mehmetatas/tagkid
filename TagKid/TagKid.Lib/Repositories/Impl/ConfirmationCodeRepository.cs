using System.Linq;
using Taga.Core.Repository;
using TagKid.Lib.Models.Entities;

namespace TagKid.Lib.Repositories.Impl
{
    public class ConfirmationCodeRepository : IConfirmationCodeRepository
    {
        private readonly IRepository _repository;

        public ConfirmationCodeRepository(IRepository repository)
        {
            _repository = repository;
        }

        public ConfirmationCode GetById(long id)
        {
            return _repository.Query<ConfirmationCode>().FirstOrDefault(cc => cc.Id == id);
        }

        public void Save(ConfirmationCode confirmationCode)
        {
            _repository.Save(confirmationCode);
        }
    }
}
