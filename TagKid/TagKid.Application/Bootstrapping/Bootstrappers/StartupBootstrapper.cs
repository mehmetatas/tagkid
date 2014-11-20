using Taga.Core.IoC;
using Taga.Core.Json;
using Taga.Core.Logging;
using Taga.Core.Mail;
using Taga.Core.Mapping;
using Taga.Core.Repository.Mapping;
using Taga.Json.Newtonsoft;
using Taga.Mapping.AutoMapper;
using TagKid.Core.Database;
using TagKid.Core.Logging;
using TagKid.Core.Mailing;
using FileMailSender = TagKid.Core.Mailing.FileMailSender;
using MailService = TagKid.Core.Mailing.MailService;

namespace TagKid.Application.Bootstrapping.Bootstrappers
{
    public class StartupBootstrapper : IBootstrapper
    {
        public void Bootstrap(IServiceProvider prov)
        {
            prov.RegisterSingleton<IMailSender, FileMailSender>();
            prov.RegisterSingleton<IMailService, MailService>();
            prov.RegisterSingleton<IMappingProvider, MappingProvider>();
            prov.RegisterSingleton<IJsonSerializer, NewtonsoftJsonSerializer>();
            prov.RegisterSingleton<IPropertyFilter, TagKidPropertyFilter>();
            prov.RegisterSingleton<IMapper, AutoMapper>();
            prov.RegisterSingleton<ILogger, DbLogger>();
        }
    }
}