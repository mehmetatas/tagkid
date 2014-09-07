using System;

namespace TagKid.Lib.Models.Entities
{
    public class User
    {
        public long Id { get; set; }

        public string Fullname { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime JoinDate { get; set; }

        public string ProfileImageUrl { get; set; }
        
        public string FacebookId { get; set; }

        public UserType Type { get; set; }

        public UserStatus Status { get; set; }
    }
}
