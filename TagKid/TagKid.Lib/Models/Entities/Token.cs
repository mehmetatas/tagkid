using System;

namespace TagKid.Lib.Models.Entities
{
    public class Token
    {
        public long Id { get; set; }

        public Guid Guid { get; set; }

        public long UserId { get; set; }

        public DateTime Expires { get; set; }

        public DateTime UsedTime { get; set; }
        
        public TokenType Type { get; set; }
    }
}
