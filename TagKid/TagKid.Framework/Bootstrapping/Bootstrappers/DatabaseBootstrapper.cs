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

            var sessionFactory = NHConnectionHelper.BuildSessionFactory<UserMap>("tagkid");

            container.RegisterTransient<IUnitOfWork, NHUnitOfWork>();
            container.RegisterTransient<IRepository, NHRepository>();
            container.RegisterTransient<IAdoRepository, NHAdoRepository>();

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
                .Map<Notification>()
                .Map<FollowUser>(fu => fu.FollowerUserId, fu => fu.FollowedUserId)
                .Map<PostLike>(pl => pl.PostId, pl => pl.UserId)
                .Map<Post>()
                .Map<PostTag>(pt => pt.PostId, pt => pt.TagId)
                .Map<TagPost>(tp => tp.TagId, tp => tp.PostId)
                .Map<PrivateMessage>()
                .Map<Tag>()
                .Map<Token>()
                .Map<User>().ToTable("[User]");

            var prov = DependencyContainer.Current.Resolve<IMappingProvider>();
            prov.SetDatabaseMapping(dbMapping);
        }
    }
}