using TagKid.Lib.PetaPoco;

namespace TagKid.Lib.Entities
{
    [TableName("tags")]
    [PrimaryKey("id", autoIncrement = true)]
    [ExplicitColumns]
    public class Tag
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("hint")]
        public string Hint { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("status")]
        public TagStatus Status { get; set; }
    }
}
