using TagKid.Lib.PetaPoco;

namespace TagKid.Lib.Entities
{
    [TableName("users")]
    [PrimaryKey("id", autoIncrement = true)]
    [ExplicitColumns]
    public class User
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("fullname")]
        public string FullName { get; set; }

        [Column("email")]
        public string Email { get; set; }
        
        [Column("password")]
        public string Password { get; set; }

        [Column("facebook_id")]
        public string FacebookId { get; set; }

        [Column("status")]
        public UserStatus Status { get; set; }
    }

    public enum UserStatus
    {
        Active,
        Passive,
        WaitingActivation,
        Banned,
        Deleted
    }
}
