using System;

namespace TagKid.Lib.Models.Entities
{
    public class Post
    {
        public virtual long Id { get; set; }

        public virtual long UserId { get; set; }

        public virtual long CategoryId { get; set; }

        public virtual long RetaggedPostId { get; set; }

        public virtual DateTime CreateDate { get; set; }

        public virtual DateTime PublishDate { get; set; }

        public virtual DateTime UpdateDate { get; set; }

        public virtual string Title { get; set; }

        public virtual string ContentCode { get; set; }

        public virtual string Content { get; set; }

        public virtual string QuoteAuthor { get; set; }

        public virtual string QuoteText { get; set; }

        public virtual string MediaEmbedUrl { get; set; }

        public virtual string LinkTitle { get; set; }

        public virtual string LinkDescription { get; set; }

        public virtual string LinkImageUrl { get; set; }

        public virtual string LinkUrl { get; set; }

        public virtual PostType Type { get; set; }

        public virtual AccessLevel AccessLevel { get; set; }

        public virtual PostStatus Status { get; set; }
    }
}