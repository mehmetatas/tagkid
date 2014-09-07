using Taga.Core.Repository.Sql;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Utils;

namespace TagKid.Lib.Repositories.Impl
{
    public class ConfirmationCodeRepository : IConfirmationCodeRepository
    {
        public ConfirmationCode GetById(long id)
        {
            return Db.SqlRepository().FirstOrDefault<ConfirmationCode>(Db.SqlBuilder()

                .Build());
        }

        public void Save(ConfirmationCode confirmationCode)
        {
            Db.SqlRepository().Save(confirmationCode);
        }
    }
}
