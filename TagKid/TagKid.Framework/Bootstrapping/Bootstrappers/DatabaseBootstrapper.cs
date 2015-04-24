using TagKid.Framework.IoC;
using TagKid.Framework.Models.Database;
using TagKid.Framework.Models.Database.Mappings;
using TagKid.Framework.Repository;
using TagKid.Framework.Repository.Impl;
using TagKid.Framework.Repository.Mapping;
using TagKid.Framework.Repository.Mapping.NamingConvention;

namespace TagKid.Framework.Bootstrapping.Bootstrappers
{
    public class DatabaseBootstrapper : IBootstrapper
    {
        public void Bootstrap(IDependencyContainer container)
        {
            InitTableMappings();

            var sessionFactory = NHSessionFactoryBuilder.Build<UserMap>("tagkid");

            container.RegisterTransient<IUnitOfWork, NHUnitOfWork>();
            container.RegisterSingleton<IRepository, NHRepository>();
            container.RegisterSingleton<IAdoRepository, NHAdoRepository>();

            container.RegisterSingleton(sessionFactory);
        }

        private static void InitTableMappings()
        {
            var dbMapping = DatabaseMapping
                .For(DbSystem.SqlServer)
                .WithNamingConvention(new SqlServerNamingConvention(false))
                .Map<Comment>()
                .Map<ConfirmationCode>()
                .Map<Login>()
                .Map<Notification>();
            dbMapping
                .Map<FollowUser>(fu => fu.FollowerUser, fu => fu.FollowedUser)
                .Map<Like>(pl => pl.Post, pl => pl.User)
                .Map<Post>()
                .Map<PostTag>(pt => pt.Post, pt => pt.Tag)
                .Map<PrivateMessage>()
                .Map<Tag>()
                .Map<Token>()
                .Map<User>();

            MappingProvider.Instance.SetDatabaseMapping(dbMapping);
        }
    }
}