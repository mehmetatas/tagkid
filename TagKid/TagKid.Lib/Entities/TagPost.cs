using TagKid.Lib.PetaPoco;

namespace TagKid.Lib.Entities
{
    [TableName("tag_posts")]
    [ExplicitColumns]
    public class TagPost
    {
        [Column("tag_id")]
        public long TagId { get; set; }

        [Column("post_id")]
        public long PostId { get; set; }
    }
}
