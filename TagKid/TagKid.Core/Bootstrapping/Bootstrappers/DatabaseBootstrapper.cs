using System.Linq;
using TagKid.Framework.IoC;
using TagKid.Core.Models.Database;
using TagKid.Core.Models.Database.Mappings;
using TagKid.Framework.Repository;
using TagKid.Framework.Repository.Fetching;
using TagKid.Framework.Repository.NH;
using TagKid.Framework.Repository.Mapping;
using TagKid.Framework.Repository.Mapping.NamingConvention;

namespace TagKid.Core.Bootstrapping.Bootstrappers
{
     class DatabaseBootstrapper : IBootstrapper
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
                .Map<Notification>()
                .Map<FollowUser>(fu => fu.FollowerUser, fu => fu.FollowedUser)
                .Map<Like>(pl => pl.Post, pl => pl.User)
                .Map<Post>()
                .Map<PostTag>(pt => pt.Post, pt => pt.Tag)
                .Map<PrivateMessage>()
                .Map<Tag>()
                .Map<Token>()
                .Map<User>();

            Fetchers.RegisterManyToMany<Post, Tag, PostTag>(p => p.Tags,
                postIds => (pt => postIds.Contains(pt.Post.Id)),
                () => pt => new ManyToManyItem<Tag> { ParentId = pt.Post.Id, Child = pt.Tag },
                (post, tags) => post.Tags = tags);

            MappingProvider.Instance.SetDatabaseMapping(dbMapping);
        }
    }
}