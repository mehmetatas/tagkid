using TagKid.Core.Models.Database;
using TagKid.Framework.Context;

namespace TagKid.Core.Context
{
    public class TagKidContext
    {
        private TagKidContext()
        {
        }

        public static TagKidContext Current 
        {
            get
            {
                var ctx = CallContext.Current["TagKidContext"] as TagKidContext;
                if (ctx == null)
                {
                    ctx = new TagKidContext();
                    CallContext.Current["TagKidContext"] = ctx;
                }
                return ctx;
            }
        }

        public User User { get; set; }
    }
}
