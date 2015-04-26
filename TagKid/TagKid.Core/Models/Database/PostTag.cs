namespace TagKid.Core.Models.Database
{
    public class PostTag
    {
        public virtual Post Post { get; set; }
        public virtual Tag Tag { get; set; }

        public override bool Equals(object obj)
        {
            var that = obj as PostTag;

            if (that == null)
            {
                return false;
            }

            return that.Post.Id == Post.Id &&
                   that.Tag.Id == Tag.Id;
        }

        public override int GetHashCode()
        {
            return Post.Id.GetHashCode() * 7907 +
                   Tag.Id.GetHashCode() * 7919;
        }
    }
}