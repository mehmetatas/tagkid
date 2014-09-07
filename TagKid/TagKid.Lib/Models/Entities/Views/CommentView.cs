
namespace TagKid.Lib.Models.Entities.Views
{
    public class CommentView : Comment
    {
        public string Fullname { get; set; }

        public string Username { get; set; }

        public string ProfileImageUrl { get; set; }
    }
}
