using System;

namespace TagKid.Lib.Models.Entities
{
    public class Token
    {
        public virtual long Id { get; set; }

        public virtual Guid Guid { get; set; }

        public virtual long UserId { get; set; }

        public virtual DateTime Expires { get; set; }

        public virtual DateTime UsedTime { get; set; }
        
        public virtual TokenType Type { get; set; }
    }
}
