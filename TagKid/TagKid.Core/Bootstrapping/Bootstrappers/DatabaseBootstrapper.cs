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
            return Db.Setup(new TagKidDbProvider())

                // Tables
                .Table<Post>()
                .Table<User>()
                .Table<Tag>()
                .Table<PostTag>()
                .Table<ConfirmationCode>()
                .Table<Like>()
                .Table<FollowUser>()
                
                // Relations
                .ManyToMany<Post, PostTag>(p => p.Tags)
                
                // Build
                .BuildFactory();
        }
    }
}