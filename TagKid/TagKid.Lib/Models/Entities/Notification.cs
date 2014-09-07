using System;

namespace TagKid.Lib.Models.Entities
{
    public class Notification
    {
        public long Id { get; set; }

        public long ToUserId { get; set; }

        public long FromUserId { get; set; }

        public long PostId { get; set; }

        public DateTime ActionDate { get; set; }

        public string Message { get; set; }

        public NotificationType Type { get; set; }

        public NotificationStatus Status { get; set; }
    }
}
