namespace TagKid.Core.Models.Database
{
    public class PostTag
    {
        public virtual long PostId { get; set; }
        public virtual long TagId { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as PostTag;
            return other != null && other.TagId == TagId && other.PostId == PostId;
        }

        public override int GetHashCode()
        {
            return TagId.GetHashCode() + PostId.GetHashCode() - 1;
        }
    }
}