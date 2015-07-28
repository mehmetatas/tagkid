using DummyOrm.Db;
using TagKid.Framework.IoC;
using TagKid.Core.Models.Database;

namespace TagKid.Core.Bootstrapping.Bootstrappers
{
    class DatabaseBootstrapper : IBootstrapper
    {
        public void Bootstrap(IDependencyContainer container)
        {
            var dbFactory = InitTableMappings();

            container.RegisterSingleton(dbFactory);
        }

        private static IDbFactory InitTableMappings()
        {
            var builder = Db.Setup(new TagKidDbProvider());

            builder.Table<Post>()
                .Table<User>()
                .Table<Tag>()
                .Table<PostTag>()
                .Table<Like>()
                .Table<FollowUser>();

            return builder
                .ManyToMany<Post, PostTag>(p => p.Tags)
                .BuildFactory();
        }
    }
}