using System.Runtime.Serialization;
using TagKid.Lib.Models.Entities;

namespace TagKid.Lib.Models.DTO.Messages
{
    [DataContract]
    public class PostRequest : Request
    {
        [DataMember(Name = "contentCode")]
        public string ContentCode { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "categoryId")]
        public long CategoryId { get; set; }

        [DataMember(Name = "tags")]
        public TagModel[] Tags { get; set; }

        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "retaggedPostId")]
        public long RetaggedPostId { get; set; }

        [DataMember(Name = "quoteAuthor")]
        public string QuoteAuthor { get; set; }

        [DataMember(Name = "quoteText")]
        public string QuoteText { get; set; }

        [DataMember(Name = "mediaEmbedUrl")]
        public string MediaEmbedUrl { get; set; }

        [DataMember(Name = "linkTitle")]
        public string LinkTitle { get; set; }

        [DataMember(Name = "linkDescription")]
        public string LinkDescription { get; set; }

        [DataMember(Name = "linkImageUrl")]
        public string LinkImageUrl { get; set; }

        [DataMember(Name = "linkUrl")]
        public string LinkUrl { get; set; }

        [DataMember(Name = "status")]
        public PostStatus Status { get; set; }

        [DataMember(Name = "type")]
        public PostType Type { get; set; }

        [DataMember(Name = "accessLevel")]
        public AccessLevel AccessLevel { get; set; }
    }
}
