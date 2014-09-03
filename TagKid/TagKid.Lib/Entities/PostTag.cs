using TagKid.Lib.PetaPoco;

namespace TagKid.Lib.Entities
{
    [TableName("post_tags")]
    [ExplicitColumns]
    public class PostTag
    {
        [Column("post_id")]
        public long PostId { get; set; }

        [Column("tag_id")]
        public long TagId { get; set; }
    }
}
