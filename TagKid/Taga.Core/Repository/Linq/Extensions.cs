using System;
using System.Linq;
using System.Text;

namespace Taga.Core.Repository.Linq
{
    public static class Extensions
    {
        public static bool EndsWith(this StringBuilder sb, string test)
        {
            if(sb.Length < test.Length)
                return false;

            string end = sb.ToString(sb.Length - test.Length, test.Length);
            return end.Equals(test);
        }

        public static StringBuilder RemoveLast(this StringBuilder sb, int length)
        {
            return sb.Remove(sb.Length - length, length);
        }

        public static bool In<T>(this T value, params T[] values)
        {
            return values.Contains(value);
        }

        public static bool Between<T>(this T value, T min, T max) where T : IComparable
        {
            return value.CompareTo(min) > -1 && value.CompareTo(max) < 1;
        }
    }
}