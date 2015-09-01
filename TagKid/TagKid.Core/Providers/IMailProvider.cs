namespace TagKid.Core.Mail
{
    public interface IMailProvider
    {
        void SendMail(MailInfo info);
    }
}