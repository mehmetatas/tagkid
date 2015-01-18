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
            var activationLink = String.Format("http://test.tagkid.com/activation/{0}/{1}", ConfirmationCode.Id,
                ConfirmationCode.Code);

            _mailSender.Send(new MailMessage
            {
                To = new[] { User.Email },
                Body = String.Format("Dear {0},<br/><br/>Thanks for joining TagKid.com!<br/><br/>You are now one step away to start tagging. You can <a href='{1}'>click here</a> to activate your TagKid.com account. If the link does not work, you can simply copy and paste the below link into your browser.<br/><br/><a href='{1}'>{1}</a><br/><br/>Sincerely,<br/>TagKid.com team.", User.Fullname, activationLink),
                Subject = "TagKid.com Activation for " + User.Fullname
            });
        }
    }
}
