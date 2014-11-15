namespace TagKid.Core.Models.Database
{
    public class PostTag
    {
        public virtual long PostId { get; set; }
        public virtual long TagId { get; set; }
    }
}