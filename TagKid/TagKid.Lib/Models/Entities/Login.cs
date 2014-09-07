using System;

namespace TagKid.Lib.Models.Entities
{
    public class Login
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public DateTime Date { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FacebookId { get; set; }

        public LoginType Type { get; set; }

        public LoginResult Result { get; set; }
    }
}
