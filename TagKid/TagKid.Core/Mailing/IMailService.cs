using TagKid.Core.Models.Database;

namespace TagKid.Core.Mailing
{
    public interface IMailService
    {
        void SendMails();

        void SendNewUserActivationMail(User user, ConfirmationCode confirmationCode);
    }
}
