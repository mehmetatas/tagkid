using System;

namespace TagKid.Lib.Models.Entities
{
    public class PrivateMessage
    {
        public virtual long Id { get; set; }

        public virtual long ToUserId { get; set; }

        public virtual long FromUserId { get; set; }

        public virtual DateTime MessageDate { get; set; }

        public virtual string Message { get; set; }

        public virtual PrivateMessageStatus Status { get; set; }
    }
}