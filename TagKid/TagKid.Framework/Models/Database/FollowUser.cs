
namespace TagKid.Framework.Models.Database
{
    public class FollowUser
    {
        public virtual long FollowerUserId { get; set; }
        public virtual long FollowedUserId { get; set; }

        public override bool Equals(object obj)
        {
            var that = obj as FollowUser;

            if (that == null)
            {
                return false;
            }

            return that.FollowerUserId == FollowerUserId &&
                   that.FollowedUserId == FollowedUserId;
        }

        public override int GetHashCode()
        {
            return FollowerUserId.GetHashCode() * 7907 +
                   FollowedUserId.GetHashCode() * 7919;
        }
    }
}
