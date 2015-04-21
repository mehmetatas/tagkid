namespace TagKid.Framework.Models.Database
{
    public class PostTag
    {
        public virtual long PostId { get; set; }
        public virtual long TagId { get; set; }

        public override bool Equals(object obj)
        {
            var that = obj as PostTag;

            if (that == null)
            {
                return false;
            }

            return that.PostId == PostId &&
                   that.TagId == TagId;
        }

        public override int GetHashCode()
        {
            return PostId.GetHashCode() * 7907 +
                   TagId.GetHashCode() * 7919;
        }
    }
}