using System;

namespace TagKid.Framework.Models.Database
{
    public class ConfirmationCode : IEntity
    {
        public virtual long Id { get; set; }
        public virtual User User { get; set; }
        public virtual string Code { get; set; }
        public virtual DateTime SendDate { get; set; }
        public virtual DateTime ExpireDate { get; set; }
        public virtual DateTime? ConfirmDate { get; set; }
        public virtual ConfirmationReason Reason { get; set; }
        public virtual ConfirmationCodeStatus Status { get; set; }
    }
}