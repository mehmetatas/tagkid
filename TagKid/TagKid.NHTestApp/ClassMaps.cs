using FluentNHibernate.Mapping;

namespace TagKid.NHTestApp
{
    public class PostMap : ClassMap<Post>
    {
        public PostMap()
        {
            Table("[Post]");

            Id(e => e.Id).GeneratedBy.Identity();

            Map(e => e.Title);

            References(e => e.User, "UserId")
                .Fetch.Join();

            HasMany(e => e.Likes)
                .KeyColumn("PostId")
                .Cascade.None()
                .Inverse();

            HasManyToMany(e => e.Tags)
                .ParentKeyColumn("PostId")
                .ChildKeyColumn("TagId")
                .Table("PostTags");
        }
    }

    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("[User]");
            Id(e => e.Id).GeneratedBy.Identity();
            Map(e => e.Username);
        }
    }

    public class TagMap : ClassMap<Tag>
    {
        public TagMap()
        {
            Table("[Tag]");
            Id(e => e.Id).GeneratedBy.Identity();
            Map(e => e.Name);
        }
    }

    public class LikeMap : ClassMap<Like>
    {
        public LikeMap()
        {
            Table("[Like]");
            CompositeId()
                .KeyReference(e => e.User, "UserId")
                .KeyReference(e => e.Post, "PostId");
        }
    }
}
