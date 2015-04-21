using System;

namespace TagKid.Framework.Models.Database
{
    public class PostLike
    {
        public virtual long PostId { get; set; }
        public virtual long UserId { get; set; }
        public virtual DateTime LikedDate { get; set; }

        public override bool Equals(object obj)
        {
            var that = obj as PostLike;

            if (that == null)
            {
                return false;
            }

            return that.PostId == PostId &&
                   that.UserId == UserId;
        }

        public override int GetHashCode()
        {
            return PostId.GetHashCode() * 7907 +
                   UserId.GetHashCode() * 7919;
        }
    }
}
