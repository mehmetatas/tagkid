using System;
using Taga.Core.Exceptions;
using Taga.Core.Logging;
using TagKid.Core.Exceptions;
using TagKid.Core.Models;
using TagKid.Core.Models.Database;

namespace TagKid.Core.Logging
{
    public static class L
    {
        public static void D(string message, string details = null)
        {
            Log(LogLevel.Debug, message, details);
        }

        public static void I(string message, string details = null)
        {
            Log(LogLevel.Info, message, details);
        }

        public static void W(string message, string details = null, Exception ex = null)
        {
            Log(LogLevel.Warning, message, details, ex);
        }

        public static void E(string message, string details = null, Exception ex = null)
        {
            Log(LogLevel.Error, message, details, ex);
        }

        private static void Log(LogLevel level, string message, string details, Exception ex = null)
        {
            var log = new Log
            {
                Level = level,
                Message = message,
                Date = DateTime.Now,
                Details = details
            };

            if (ex != null)
            {
                log.Details += "|" + ex.GetMessages() + "|" + ex.StackTrace;
                if (ex is TagKidException)
                {
                    log.ErrorCode = ((TagKidException) ex).ErrorCode.ToString();
                }
            }

            if (RequestContext.Current.User != null)
            {
                log.User = RequestContext.Current.User.Id.ToString();
            }

            LogScope.Current.Log(log);
        }
    }
}
