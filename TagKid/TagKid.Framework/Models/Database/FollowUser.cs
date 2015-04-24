
namespace TagKid.Framework.Models.Database
{
    public class FollowUser
    {
        public virtual User FollowerUser { get; set; }
        public virtual User FollowedUser { get; set; }

        public override bool Equals(object obj)
        {
            var that = obj as FollowUser;

            if (that == null)
            {
                return false;
            }

            return that.FollowerUser.Id == FollowerUser.Id &&
                   that.FollowedUser.Id == FollowedUser.Id;
        }

        public override int GetHashCode()
        {
            return FollowerUser.Id.GetHashCode() * 7907 +
                   FollowedUser.Id.GetHashCode() * 7919;
        }
    }
}
