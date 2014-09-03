using System;
using TagKid.Lib.PetaPoco;

namespace TagKid.Lib.Entities
{
    [TableName("logins")]
    [PrimaryKey("id", autoIncrement = true)]
    [ExplicitColumns]
    public class Login
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("facebook_id")]
        public string FacebookId { get; set; }

        [Column("type")]
        public LoginType Type { get; set; }

        [Column("result")]
        public LoginResult Result { get; set; }
    }
}
