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
        public static void Dbg(string message, string details = null)
        {
            Log(LogLevel.Debug, message, details);
        }

        public static void Inf(string message, string details = null)
        {
            Log(LogLevel.Info, message, details);
        }

        public static void Wrn(string message, Exception ex = null, string details = null)
        {
            Log(LogLevel.Warning, message, details, ex);
        }

        public static void Err(string message, Exception ex = null, string details = null)
        {
            Log(LogLevel.Error, message, details, ex);
        }

        public static void Flush(LogLevel minLogLevel, LogLevel treshhold)
        {
            try
            {
                LogScope.Current.Flush(minLogLevel, treshhold);
            }
            catch
            {
                
            }
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
                var tagKidException = ex as TagKidException;
                if (tagKidException != null)
                {
                    log.ErrorCode = tagKidException.Error.Code.ToString();
                }
            }

            if (RequestContext.Current.Authenticated)
            {
                log.User = RequestContext.Current.AuthToken.User.Id.ToString();
            }

            LogScope.Current.Log(log);
        }
    }
}
