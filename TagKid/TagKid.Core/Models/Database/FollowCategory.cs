
namespace TagKid.Core.Models.Database
{
    public class FollowCategory
    {
        public virtual long UserId { get; set; }
        public virtual long CategoryId { get; set; }

        public override bool Equals(object obj)
        {
            var that = obj as FollowCategory;

            if (that == null)
            {
                return false;
            }

            return that.UserId == UserId &&
                   that.CategoryId == CategoryId;
        }

        public override int GetHashCode()
        {
            return UserId.GetHashCode() * 7907 +
                   CategoryId.GetHashCode() * 7919;
        }
    }
}
