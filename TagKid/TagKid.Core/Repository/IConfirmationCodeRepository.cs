using TagKid.Core.Models.Database;

namespace TagKid.Core.Repository
{
    public interface IConfirmationCodeRepository
    {
        void Save(ConfirmationCode confCode);
        ConfirmationCode GetForActivation(long id);
    }
}
