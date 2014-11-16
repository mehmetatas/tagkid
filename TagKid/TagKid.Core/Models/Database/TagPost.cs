namespace TagKid.Core.Models.Database
{
    public class TagPost
    {
        public virtual long TagId { get; set; }
        public virtual long PostId { get; set; }

        public override bool Equals(object obj)
        {
            var that = obj as TagPost;

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