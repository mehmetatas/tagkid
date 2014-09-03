using System;
using TagKid.Lib.PetaPoco;

namespace TagKid.Lib.Entities
{
    [TableName("posts")]
    [PrimaryKey("id", autoIncrement = true)]
    [ExplicitColumns]
    public class Post
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }

        [Column("category_id")]
        public long CategoryId { get; set; }

        [Column("retagged_post_id")]
        public long RetaggedPostId { get; set; }

        [Column("create_date")]
        public DateTime CreateDate { get; set; }

        [Column("publish_date")]
        public DateTime PublishDate { get; set; }

        [Column("update_date")]
        public DateTime UpdateDate { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("content")]
        public string Content { get; set; }

        [Column("quote_author")]
        public string QuoteAuthor { get; set; }

        [Column("quote_text")]
        public string QuoteText { get; set; }

        [Column("media_embed_url")]
        public string MediaEmbedUrl { get; set; }

        [Column("link_title")]
        public string LinkTitle { get; set; }

        [Column("link_description")]
        public string LinkDescription { get; set; }

        [Column("link_image_url")]
        public string LinkImageUrl { get; set; }

        [Column("link_url")]
        public string LinkUrl { get; set; }

        [Column("type")]
        public PostType Type { get; set; }

        [Column("access_level")]
        public AccessLevel AccessLevel { get; set; }

        [Column("status")]
        public PostStatus Status { get; set; }
    }
}
