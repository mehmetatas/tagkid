namespace TagKid.Lib.Models.Entities.Views
{
    public class CommentView : Comment
    {
        public virtual string Fullname { get; set; }

        public virtual string Username { get; set; }

        public virtual string ProfileImageUrl { get; set; }
    }
}