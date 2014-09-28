using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Taga.Core.IoC;
using Taga.Core.Repository;
using Taga.Core.Repository.Sql;
using TagKid.Lib.PetaPoco;
using TagKid.Lib.PetaPoco.Repository;
using TagKid.Lib.PetaPoco.Repository.Sql;
using TagKid.Lib.Repositories;
using TagKid.Lib.Repositories.Impl;
using TagKid.Lib.Services;
using TagKid.Lib.Services.Impl;
using IMapper = Taga.Core.Repository.IMapper;

namespace TagKid.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RegisterServices();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        private void RegisterServices()
        {
            var prov = ServiceProvider.Provider;

            prov.Register<ISqlRepository, PetaPocoSqlRepository>();
            prov.Register<IUnitOfWork, PetaPocoUnitOfWork>();
            prov.Register<ISqlBuilder, PetaPocoSqlBuilder>();

            prov.Register<IAuthService, AuthService>();
            prov.Register<IPostService, PostService>();
            prov.Register<ITagService, TagService>();

            prov.Register<ITagRepository, TagRepository>();
            prov.Register<IUserRepository, UserRepository>();
            prov.Register<ILoginRepository, LoginRepository>();
            prov.Register<IPostRepository, PostRepository>();
            prov.Register<ICommentRepository, CommentRepository>();
            prov.Register<INotificationRepository, NotificationRepository>();
            prov.Register<IPrivateMessageRepository, PrivateMessageRepository>();
            prov.Register<ITokenRepository, TokenRepository>();

            prov.Register<IMapper, TagKidMapper>(new TagKidMapper());
        }
    }
}
