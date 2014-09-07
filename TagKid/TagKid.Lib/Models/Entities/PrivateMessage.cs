using System;

namespace TagKid.Lib.Models.Entities
{
    public class PrivateMessage
    {
        public long ToUserId { get; set; }

        public long FromUserId { get; set; }

        public DateTime MessageDate { get; set; }

        public string Message { get; set; }

        public PrivateMessageStatus Status { get; set; }
    }
}
