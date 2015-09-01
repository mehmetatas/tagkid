using System.Globalization;
using TagKid.Framework.Utils;

namespace TagKid.Core.Mail
{
    public class MailInfo
    {
        public MailInfo(string templateCode)
        {
            TemplateCode = templateCode;
        }

        public string TemplateCode { get; }
        public string To { get; set; }
        public CultureInfo Culture { get; set; } = Cultures.EnGb;
        public IMailTemplateData TemplateData { get; set; }

        public static MailInfo NewUserActivation(string to, IMailTemplateData data)
        {
            return new MailInfo("NewUserActivation") { To = to, TemplateData = data };
        }
    }
}