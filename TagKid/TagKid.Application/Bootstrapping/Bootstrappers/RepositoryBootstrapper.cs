using Taga.Core.IoC;
using TagKid.Core.Repository;
using TagKid.Repository;

namespace TagKid.Application.Bootstrapping.Bootstrappers
{
    public class RepositoryBootstrapper : IBootstrapper
    {
        public void Bootstrap(IServiceProvider prov)
        {
            prov.Register<ICategoryRepository, CategoryRepository>();
            prov.Register<ICommentRepository, CommentRepository>();
            prov.Register<IConfirmationCodeRepository, ConfirmationCodeRepository>();
            prov.Register<ILoginRepository, LoginRepository>();
            prov.Register<INotificationRepository, NotificationRepository>();
            prov.Register<IPostRepository, PostRepository>();
            prov.Register<IPrivateMessageRepository, PrivateMessageRepository>();
            prov.Register<ITagRepository, TagRepository>();
            prov.Register<ITokenRepository, TokenRepository>();
            prov.Register<IUserRepository, UserRepository>();

            prov.Register<IRepositoryProvider, RepositoryProvider>();
        }
    }
}