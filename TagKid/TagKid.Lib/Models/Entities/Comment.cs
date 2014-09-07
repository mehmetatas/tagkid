using System;

namespace TagKid.Lib.Models.Entities
{
    public class Comment
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long PostId { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public string Content { get; set; }

        public CommentStatus Status { get; set; }
    }
}
