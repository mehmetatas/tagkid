using TagKid.Lib.Models.Entities;

namespace TagKid.Lib.Repositories
{
    public interface IConfirmationCodeRepository
    {
        ConfirmationCode GetById(long id);

        void Save(ConfirmationCode confirmationCode);
    }
}
