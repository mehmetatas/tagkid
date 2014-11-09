using System;

namespace TagKid.Core.Models.Database
{
    public class Comment
    {
        public virtual long Id { get; set; }
        public virtual long PostId { get; set; }
        public virtual long UserId { get; set; }
        public virtual DateTime PublishDate { get; set; }
        public virtual DateTime UpdateDate { get; set; }
        public virtual string Content { get; set; }
        public virtual CommentStatus Status { get; set; }

        public virtual User User { get; set; }
    }
}