using System.Linq;
using Taga.Core.Repository;
using TagKid.Core.Models.Database;
using TagKid.Core.Repository;

namespace TagKid.Repository
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
            return _repository.Select<ConfirmationCode>().FirstOrDefault(cc => cc.Id == id);
        }

        public void Save(ConfirmationCode confirmationCode)
        {
            _repository.Save(confirmationCode);
        }
    }
}