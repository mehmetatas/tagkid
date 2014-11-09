using TagKid.Core.Models.Database;

namespace TagKid.Core.Repositories
{
    public interface IConfirmationCodeRepository : ITagKidRepository
    {
        ConfirmationCode GetById(long id);

        void Save(ConfirmationCode confirmationCode);
    }
}