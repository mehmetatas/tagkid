using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using Taga.Core.Mail;
using TagKid.Core.Mailing.Commands;
using TagKid.Core.Models.Database;
using MailMessage = Taga.Core.Mail.MailMessage;

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
            // Commands.Add(new NewUserActivationMail(user, confirmationCode));
            var cmd = new NewUserActivationMail(user, confirmationCode);
            cmd.Send();
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

    public class SmtpMailSender : IMailSender
    {
        public void Send(MailMessage mailMessage)
        {
            var client = new SmtpClient("mail.turpgames.com", 587);
            
            client.Credentials = new NetworkCredential("tagkid@turpgames.com", "Tag1234Kid");

            client.Send(new System.Net.Mail.MailMessage("tagkid@turpgames.com", mailMessage.To[0], mailMessage.Subject, mailMessage.Body)
            {
                IsBodyHtml = true
            });
        }
    }
}
