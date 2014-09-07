using TagKid.Lib.PetaPoco;

namespace TagKid.Lib.Entities.Views
{
    [TableName("comment_view")]
    [ExplicitColumns]
    public class CommentView : Comment
    {
        [Column("fullname")]
        public string FullName { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("profile_image_url")]
        public string ProfileImageUrl { get; set; }
    }
}
