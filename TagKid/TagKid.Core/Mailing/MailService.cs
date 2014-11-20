using System;
using System.Collections.Generic;
using System.IO;
using Taga.Core.Mail;
using TagKid.Core.Mailing.Commands;
using TagKid.Core.Models.Database;

namespace TagKid.Core.Mailing
{
    public class MailService : IMailService
    {
        [ThreadStatic]
        private static List<IMailCommand> _commands;

        public List<IMailCommand> Commands
        {
            get { return _commands ?? (_commands = new List<IMailCommand>()); }
        }

        public void SendNewUserActivationMail(User user, ConfirmationCode confirmationCode)
        {
            Commands.Add(new NewUserActivationMail(user, confirmationCode));
        }

        public void SendMails()
        {
            foreach (var mail in Commands)
            {
                mail.Send();
            }
        }
    }

    public class FileMailSender : IMailSender
    {
        public void Send(MailMessage mailMessage)
        {
            File.WriteAllText(
                String.Format(@"C:\{0}.txt", mailMessage.Subject),
                String.Format("To : {0}{1}{2}", mailMessage.To[0], Environment.NewLine, mailMessage.Body));
        }
    }
}
