using Taga.Core.Repository.Linq;
using TagKid.Lib.Entities;
using TagKid.Lib.Utils;

namespace TagKid.Lib.Repositories.Impl
{
    public class ConfirmationCodeRepository : IConfirmationCodeRepository
    {
        public ConfirmationCode GetById(long id)
        {
            return Db.LinqRepository().FirstOrDefault<ConfirmationCode>(cc => cc.Id == id);
        }

        public void Save(ConfirmationCode confirmationCode)
        {
            Db.LinqRepository().Save(confirmationCode);
        }
    }
}
