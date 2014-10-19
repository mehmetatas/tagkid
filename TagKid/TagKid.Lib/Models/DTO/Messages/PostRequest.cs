using TagKid.Lib.Models.Entities;

namespace TagKid.Lib.Models.DTO.Messages
{
    public class PostRequest : Request
    {
        public string ContentCode { get; set; }

        public string Title { get; set; }

        public long CategoryId { get; set; }

        public TagModel[] Tags { get; set; }

        public long Id { get; set; }

        public long RetaggedPostId { get; set; }

        public string QuoteAuthor { get; set; }

        public string QuoteText { get; set; }

        public string MediaEmbedUrl { get; set; }

        public string LinkTitle { get; set; }

        public string LinkDescription { get; set; }

        public string LinkImageUrl { get; set; }

        public string LinkUrl { get; set; }

        public PostStatus Status { get; set; }

        public PostType Type { get; set; }

        public AccessLevel AccessLevel { get; set; }
    }
}
