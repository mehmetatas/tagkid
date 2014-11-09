using System;
using System.Text;

namespace TagKid.Core.Exceptions
{
    public static class ExceptionExtensions
    {
        public static string GetMessage(this Exception e)
        {
            var sb = new StringBuilder();
            while (e != null)
            {
                sb.AppendLine(e.Message);
                e = e.InnerException;
            }
            return sb.ToString();
        }
    }
}