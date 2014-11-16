
using TagKid.Core.Models.Database;

namespace TagKid.Core.Models.Domain
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

        public string LinkTitle
        {
            get { return Post.LinkTitle; }
            set { Post.LinkTitle = value; }
        }

        public string LinkDescription
        {
            get { return Post.LinkDescription; }
            set { Post.LinkDescription = value; }
        }

        public string LinkImageUrl
        {
            get { return Post.LinkImageUrl; }
            set { Post.LinkImageUrl = value; }
        }

        public string LinkUrl
        {
            get { return Post.LinkUrl; }
            set { Post.LinkUrl = value; }
        }
    }
}
