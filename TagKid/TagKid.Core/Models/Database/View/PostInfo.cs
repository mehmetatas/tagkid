namespace TagKid.Core.Models.Database.View
{
    public class PostInfo
    {
        public virtual long PostId { get; set; }
        public virtual int LikeCount { get; set; }
        public virtual int CommentCount { get; set; }
        public virtual bool Liked { get; set; }
        //public virtual int RetagCount { get; set; }
        //public virtual bool Retaged { get; set; }
    }
}