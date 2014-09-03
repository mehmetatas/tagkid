using System;
using TagKid.Lib.PetaPoco;

namespace TagKid.Lib.Entities
{
    [TableName("confirmation_codes")]
    [PrimaryKey("id", autoIncrement = true)]
    [ExplicitColumns]
    public class ConfirmationCode
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("send_date")]
        public DateTime SendDate { get; set; }

        [Column("expire_date")]
        public DateTime ExpireDate { get; set; }

        [Column("reason")]
        public ConfirmationReason Reason { get; set; }

        [Column("status")]
        public ConfirmationCodeStatus Status { get; set; }
    }
}
