namespace TagKid.Core.Models.Database.Proxies
{
    public class RetagPost : PostProxy
    {
        public RetagPost()
        {
        }

        public RetagPost(Post post)
            : base(post)
        {
            RetaggedPost = For(post);
        }

        public virtual long RetaggedPostId
        {
            get { return Post.RetaggedPostId; }
            set { Post.RetaggedPostId = value; }
        }

        public virtual PostProxy RetaggedPost { get; set; }
    }
}