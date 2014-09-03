using System;
using TagKid.Lib.PetaPoco;

namespace TagKid.Lib.Entities
{
    [TableName("notifications")]
    [PrimaryKey("id", autoIncrement = true)]
    [ExplicitColumns]
    public class Notification
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("to_user_id")]
        public long ToUserId { get; set; }

        [Column("from_user_id")]
        public long FromUserId { get; set; }

        [Column("post_id")]
        public long PostId { get; set; }

        [Column("action_date")]
        public DateTime ActionDate { get; set; }

        [Column("message")]
        public string Message { get; set; }

        [Column("type")]
        public NotificationType Type { get; set; }

        [Column("status")]
        public NotificationStatus Status { get; set; }
    }
}
