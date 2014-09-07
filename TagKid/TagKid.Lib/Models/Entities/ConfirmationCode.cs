using System;

namespace TagKid.Lib.Models.Entities
{
    public class ConfirmationCode
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public string Code { get; set; }

        public DateTime SendDate { get; set; }

        public DateTime ExpireDate { get; set; }

        public ConfirmationReason Reason { get; set; }

        public ConfirmationCodeStatus Status { get; set; }
    }
}
