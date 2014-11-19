using System;

namespace TagKid.Core.Models.Database
{
    public class Token
    {
        public virtual long Id { get; set; }
        public virtual string Guid { get; set; }
        public virtual long UserId { get; set; }
        public virtual DateTime ExpireDate { get; set; }
        public virtual DateTime? UseDate { get; set; }
        public virtual TokenType Type { get; set; }
    }
}