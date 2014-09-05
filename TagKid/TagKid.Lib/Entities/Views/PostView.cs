using TagKid.Lib.PetaPoco;

namespace TagKid.Lib.Entities.Views
{
    [TableName("post_search_view")]
    [ExplicitColumns]
    public class PostView : Post
    {
        [Column("fullname")]
        public string FullName { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("category_name")]
        public string CategoryName { get; set; }

        [Column("category_access_level")]
        public AccessLevel CategoryAccessLevel { get; set; }
    }
}
