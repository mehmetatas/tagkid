
namespace TagKid.Core.Models.Database.Proxies
{
    public class LinkPost : PostProxy
    {
        public LinkPost()
        {
            
        }

        public LinkPost(Post post)
            : base(post)
        {
            
        }

        public virtual string LinkTitle
        {
            get { return Post.LinkTitle; }
            set { Post.LinkTitle = value; }
        }

        public virtual string LinkDescription
        {
            get { return Post.LinkDescription; }
            set { Post.LinkDescription = value; }
        }

        public virtual string LinkImageUrl
        {
            get { return Post.LinkImageUrl; }
            set { Post.LinkImageUrl = value; }
        }

        public virtual string LinkUrl
        {
            get { return Post.LinkUrl; }
            set { Post.LinkUrl = value; }
        }
    }
}
