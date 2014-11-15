using System;

namespace TagKid.Core.Models.Database
{
    public class PostLike
    {
        public virtual long PostId { get; set; }
        public virtual long UserId { get; set; }
        public virtual DateTime LikedDate { get; set; }
    }
}
