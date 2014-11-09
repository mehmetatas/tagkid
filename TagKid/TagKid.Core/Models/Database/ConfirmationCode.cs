﻿using System;

namespace TagKid.Core.Models.Database
{
    public class ConfirmationCode
    {
        public virtual long Id { get; set; }
        public virtual long UserId { get; set; }
        public virtual string Code { get; set; }
        public virtual DateTime SendDate { get; set; }
        public virtual DateTime ExpireDate { get; set; }
        public virtual DateTime ConfirmDate { get; set; }
        public virtual ConfirmationReason Reason { get; set; }
        public virtual ConfirmationCodeStatus Status { get; set; }
    }
}