using TagKid.Core.Models.Database;

namespace TagKid.Core.Mailing
{
    public interface IMailService
    {
        void SendNewUserActivationMail(User user, ConfirmationCode confirmationCode);
    }
}
