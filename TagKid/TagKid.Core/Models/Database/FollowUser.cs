﻿
namespace TagKid.Core.Models.Database
{
    public class FollowUser
    {
        public virtual User FollowerUser { get; set; }
        public virtual User FollowedUser { get; set; }
    }
}
