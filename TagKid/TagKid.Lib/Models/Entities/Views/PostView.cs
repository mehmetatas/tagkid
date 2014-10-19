
namespace TagKid.Lib.Models.Entities.Views
{
    public class PostView : Post
    {
        public virtual string Fullname { get; set; }

        public virtual string Username { get; set; }

        public virtual string ProfileImageUrl { get; set; }

        public virtual string CategoryName { get; set; }

        public virtual AccessLevel CategoryAccessLevel { get; set; }
    }
}
