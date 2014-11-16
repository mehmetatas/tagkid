using TagKid.Core.Models.Database;

namespace TagKid.Core.Repository
{
    public interface IConfirmationCodeRepository : ITagKidRepository
    {
        ConfirmationCode GetById(long id);

        void Save(ConfirmationCode confirmationCode);
    }
}