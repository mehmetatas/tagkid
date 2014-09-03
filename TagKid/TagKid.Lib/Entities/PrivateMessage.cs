using System;
using TagKid.Lib.PetaPoco;

namespace TagKid.Lib.Entities
{
    [TableName("private_messages")]
    [ExplicitColumns]
    public class PrivateMessage
    {
        [Column("to_user_id")]
        public long ToUserId { get; set; }

        [Column("from_user_id")]
        public long FromUserId { get; set; }

        [Column("message_date")]
        public DateTime MessageDate { get; set; }

        [Column("message")]
        public string Message { get; set; }

        [Column("status")]
        public PrivateMessageStatus Status { get; set; }
    }
}
