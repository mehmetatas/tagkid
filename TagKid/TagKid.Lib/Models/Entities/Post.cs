using System;

namespace TagKid.Lib.Models.Entities
{
    public class Post
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long CategoryId { get; set; }

        public long RetaggedPostId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public string Title { get; set; }

        public string ContentCode { get; set; }

        public string Content { get; set; }

        public string QuoteAuthor { get; set; }

        public string QuoteText { get; set; }

        public string MediaEmbedUrl { get; set; }

        public string LinkTitle { get; set; }

        public string LinkDescription { get; set; }

        public string LinkImageUrl { get; set; }

        public string LinkUrl { get; set; }

        public PostType Type { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public PostStatus Status { get; set; }
    }
}
