using System;
using Taga.Core.IoC;
using Taga.Core.Mail;
using TagKid.Core.Models.Database;

namespace TagKid.Core.Mailing.Commands
{
    public class NewUserActivationMail : IMailCommand
    {
        private readonly IMailSender _mailSender;

        public NewUserActivationMail(User user, ConfirmationCode confirmationCode)
        {
            _mailSender = ServiceProvider.Provider.GetOrCreate<IMailSender>();
            User = user;
            ConfirmationCode = confirmationCode;
        }

        public User User { get; private set; }
        public ConfirmationCode ConfirmationCode { get; private set; }

        public void Send()
        {
            _mailSender.Send(new MailMessage
            {
                To = new[] { User.Email },
                Body = String.Format("http://localhost:53495/api/auth/activateAccount?ccid={0}&cc={1}", ConfirmationCode.Id, ConfirmationCode.Code),
                Subject = "TagKid Activation"
            });
        }
    }
}
