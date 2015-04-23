using System.Collections.Generic;

namespace TagKid.NHTestApp
{
    public class Post
    {
        public virtual long Id { get; set; }
        public virtual string Title { get; set; }
        public virtual User User { get; set; }
        public virtual IList<Like> Likes { get; set; }
        public virtual IList<Tag> Tags { get; set; }
    }

    public class User
    {
        public virtual long Id { get; set; }
        public virtual string Username { get; set; }
    }

    public class Tag
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
    }

    public class Like
    {
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    //public class PostTag
    //{
    //    public virtual Post Post { get; set; }
    //    public virtual Tag Tag { get; set; }
    //}
}