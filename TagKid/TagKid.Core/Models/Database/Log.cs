using System;
using TagKid.Framework.Logging;

namespace TagKid.Core.Models.Database
{
    public class Log : ILog
    {
        public virtual long Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual LogLevel Level { get; set; }
        public virtual string ErrorCode { get; set; }
        public virtual string Message { get; set; }
        public virtual string User { get; set; }
        public virtual string Details { get; set; }
    }
}
