using FluentNHibernate.Mapping;

namespace TagKid.NHTestApp
{
    public class PostMap : ClassMap<Post>
    {
        public PostMap()
        {
            Id(e => e.Id).GeneratedBy.Identity();

            Map(e => e.Title);

            References(e => e.User, "UserId")
                .Fetch.Join();

            //HasMany(e => e.Likes)
            //    .KeyColumn("PostId")
            //    .Cascade.None()
            //    .Inverse();

            //HasManyToMany(e => e.Tags)
            //    .ParentKeyColumn("PostId")
            //    .ChildKeyColumn("TagId")
            //    .Table("PostTag");
        }
    }

    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(e => e.Id).GeneratedBy.Identity();
            Map(e => e.Username);
        }
    }

    public class TagMap : ClassMap<Tag>
    {
        public TagMap()
        {
            Id(e => e.Id).GeneratedBy.Identity();
            Map(e => e.Name);
        }
    }

    public class LikeMap : ClassMap<Like>
    {
        public LikeMap()
        {
            CompositeId()
                .KeyReference(e => e.User, "UserId")
                .KeyReference(e => e.Post, "PostId");
        }
    }

    public class PostTagMap : ClassMap<PostTag>
    {
        public PostTagMap()
        {
            CompositeId()
                .KeyReference(e => e.Tag, "TagId")
                .KeyReference(e => e.Post, "PostId");
        }
    }
}
