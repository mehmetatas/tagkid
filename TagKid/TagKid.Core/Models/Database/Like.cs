using System;

namespace TagKid.Core.Models.Database
{
    public class Like
    {
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
        public virtual DateTime LikedDate { get; set; }

        public override bool Equals(object obj)
        {
            var that = obj as Like;

            if (that == null)
            {
                return false;
            }

            return that.Post.Id == Post.Id &&
                   that.User.Id == User.Id;
        }

        public override int GetHashCode()
        {
            return Post.Id.GetHashCode() * 7907 +
                   User.Id.GetHashCode() * 7919;
        }
    }
}
