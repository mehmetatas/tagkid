﻿using System;

namespace TagKid.Framework.Models.Database
{
    public class Token
    {
        public virtual long Id { get; set; }
        public virtual Guid Guid { get; set; }
        public virtual long UserId { get; set; }
        public virtual DateTime ExpireDate { get; set; }
        public virtual DateTime? UseDate { get; set; }
        public virtual TokenType Type { get; set; }

        public virtual User User { get; set; }
    }
}