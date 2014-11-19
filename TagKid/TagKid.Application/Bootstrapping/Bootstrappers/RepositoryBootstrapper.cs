using Taga.Core.IoC;
using TagKid.Core.Repository;
using TagKid.Repository;

namespace TagKid.Application.Bootstrapping.Bootstrappers
{
    public class RepositoryBootstrapper : IBootstrapper
    {
        public void Bootstrap(IServiceProvider prov)
        {
            prov.RegisterPerWebRequest<ICategoryRepository, CategoryRepository>();
            prov.RegisterPerWebRequest<ICommentRepository, CommentRepository>();
            prov.RegisterPerWebRequest<IConfirmationCodeRepository, ConfirmationCodeRepository>();
            prov.RegisterPerWebRequest<ILoginRepository, LoginRepository>();
            prov.RegisterPerWebRequest<INotificationRepository, NotificationRepository>();
            prov.RegisterPerWebRequest<IPostRepository, PostRepository>();
            prov.RegisterPerWebRequest<IPrivateMessageRepository, PrivateMessageRepository>();
            prov.RegisterPerWebRequest<ITagRepository, TagRepository>();
            prov.RegisterPerWebRequest<ITokenRepository, TokenRepository>();
            prov.RegisterPerWebRequest<IUserRepository, UserRepository>();
        }
    }
}