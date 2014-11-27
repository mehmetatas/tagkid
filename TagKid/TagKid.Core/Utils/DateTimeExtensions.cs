using System;

namespace TagKid.Core.Utils
{
    public static class DateTimeExtensions
    {
        private static readonly DateTime SqlServerMindDateTime = new DateTime(1753, 1, 1);

        public static DateTime TrimMillis(this DateTime dateTime)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                dateTime.Hour,
                dateTime.Minute,
                dateTime.Second,
                dateTime.Kind);
        }

        public static DateTime EnsureSqlServerMin(this DateTime dateTime)
        {
            return dateTime == DateTime.MinValue ? SqlServerMindDateTime : dateTime;
        }

        public static bool IsSqlServerMin(this DateTime dateTime)
        {
            return dateTime == SqlServerMindDateTime;
        }
    }
}
