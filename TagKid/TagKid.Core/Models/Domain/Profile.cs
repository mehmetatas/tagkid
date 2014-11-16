using System;
using TagKid.Core.Models.Database;

namespace TagKid.Core.Models.Domain
{
    public class Profile
    {
        private readonly User _user;

        public Profile(User user, int followerCount, int followingCount, int postCount, int categoryCount)
        {
            _user = user;
            CategoryCount = categoryCount;
            PostCount = postCount;
            FollowingCount = followingCount;
            FollowerCount = followerCount;
        }

        public long Id
        {
            get { return _user.Id; }
        }

        public string Username
        {
            get { return _user.Username; }
        }

        public string Fullname
        {
            get { return _user.Fullname; }
        }

        public DateTime JoinDate
        {
            get { return _user.JoinDate; }
        }

        public int FollowerCount { get; private set; }

        public int FollowingCount { get; private set; }

        public int PostCount { get; private set; }

        public int CategoryCount { get; private set; }
    }
}
