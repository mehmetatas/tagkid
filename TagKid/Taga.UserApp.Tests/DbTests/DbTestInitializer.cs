using Taga.Core.IoC;
using Taga.Core.Repository;
using Taga.Core.Repository.Mapping;
using Taga.UserApp.Core.Database;
using Taga.UserApp.Core.Database.NamingConvention;
using Taga.UserApp.Core.Model.Database;
using Taga.UserApp.Core.Repository;
using Taga.UserApp.Repository;
using Taga.UserApp.Tests.Mocks;

namespace Taga.UserApp.Tests.DbTests
{
    public class DbTestInitializer
    {
        public static void Initialize(bool sqlServer)
        {
            InitServiceProvider();

            RegisterRepositories();

            ServiceProvider.Provider.RegisterSingleton<IMappingProvider>(new MappingProvider());

            InitMappings(sqlServer);
        }

        private static void InitMappings(bool sqlServer)
        {
            var namingConvention = sqlServer
                ? (IDatabaseNamingConvention) new SqlServerNamingConvention(true)
                : (IDatabaseNamingConvention) new MySqlNamingConvention(true);

            var dbMapping = DatabaseMapping.WithNamingConvention(namingConvention)
                .Map<User>()
                .Map<Role>()
                .Map<Permission>()
                .Map<UserRole>(ur => ur.UserId, ur => ur.RoleId)
                .Map<RolePermission>(rp => rp.RoleId, rp => rp.PermissionId)
                .Map<Category>()
                .Map<Post>()
                .Map<TextPost>(p => p.PostId)
                .Map<ImagePost>(p => p.PostId)
                .Map<VideoPost>(p => p.PostId)
                .Map<QuotePost>(p => p.PostId);

            var mappingProv = ServiceProvider.Provider.GetOrCreate<IMappingProvider>();

            mappingProv.SetDatabaseMapping(dbMapping);
        }

        private static void InitServiceProvider()
        {
            if (ServiceProvider.Provider == null)
            {
                ServiceProvider.Provider = new TestServiceProvider();
            }
            else
            {
                var mockProv = (TestServiceProvider)ServiceProvider.Provider;
                mockProv.Reset();
            }
        }

        private static void RegisterRepositories()
        {
            var prov = ServiceProvider.Provider;

            prov.Register<IUserRepository, UserRepository>();
            prov.Register<IAuthorizationRepository, AuthorizationRepository>();
            prov.Register<IPostRepository, PostRepository>();
        }

        public static void ClearDb(bool ef)
        {
            using (Db.Readonly())
            {
                var repo = ServiceProvider.Provider.GetOrCreate<IRepository>();
                if (ef)
                {
                    repo.ExecSp<object>("truncate_all");
                }
                else
                {
                    repo.ExecSp<User>("truncate_all");
                }
            }
        }
    }
}
