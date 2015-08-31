using System;

namespace TagKid.Core.Mail
{
    public interface IMailTemplateData
    {
    }

    public class NewUserActivationTemplateData : IMailTemplateData
    {
        public string Username { get; set; }
        public long ConfirmationCodeId { get; set; }
        public Guid ConfirmationCode { get; set; }
    }
}
