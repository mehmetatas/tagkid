
using TagKid.Lib.PetaPoco;

namespace TagKid.Lib.Entities.Views
{
    [TableName("user_tags_view")]
    [ExplicitColumns]
    public class UserTagsView : Tag
    {
        [Column("tag_count")]
        public int TagCount { get; set; }
    }
}
