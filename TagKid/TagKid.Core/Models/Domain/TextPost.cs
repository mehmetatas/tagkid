
using TagKid.Core.Models.Database;

namespace TagKid.Core.Models.Domain
{
    public class TextPost : PostProxy
    {
        public TextPost()
        {
        }

        public TextPost(Post post)
            : base(post)
        {
        }
    }
}
