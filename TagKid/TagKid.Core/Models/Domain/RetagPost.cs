using TagKid.Core.Models.Database;

namespace TagKid.Core.Models.Domain
{
    public class RetagPost : PostProxy
    {
        public RetagPost()
        {
        }

        public RetagPost(Post post)
            : base(post)
        {
            RetaggedPost = Create(post);
        }

        public virtual long? RetaggedPostId
        {
            get { return Post.RetaggedPostId; }
            set { Post.RetaggedPostId = value; }
        }

        public virtual PostProxy RetaggedPost { get; set; }
    }
}