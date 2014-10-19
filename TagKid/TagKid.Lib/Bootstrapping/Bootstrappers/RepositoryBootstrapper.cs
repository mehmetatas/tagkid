using Taga.Core.IoC;
using TagKid.Lib.Repositories;
using TagKid.Lib.Repositories.Impl;

namespace TagKid.Lib.Bootstrapping.Bootstrappers
{
    public class RepositoryBootstrapper : IBootstrapper
    {
        public void Bootstrap(IServiceProvider prov)
        {
            prov.Register<ITagRepository, TagRepository>();
            prov.Register<IUserRepository, UserRepository>();
            prov.Register<ILoginRepository, LoginRepository>();
            prov.Register<IPostRepository, PostRepository>();
            prov.Register<ICommentRepository, CommentRepository>();
            prov.Register<INotificationRepository, NotificationRepository>();
            prov.Register<IPrivateMessageRepository, PrivateMessageRepository>();
            prov.Register<ITokenRepository, TokenRepository>();
        }
    }
}