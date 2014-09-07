
namespace TagKid.Lib.Models.Entities.Views
{
    public class PostView : Post
    {
        public string Fullname { get; set; }

        public string Username { get; set; }

        public string ProfileImageUrl { get; set; }

        public string CategoryName { get; set; }

        public AccessLevel CategoryAccessLevel { get; set; }
    }
}
