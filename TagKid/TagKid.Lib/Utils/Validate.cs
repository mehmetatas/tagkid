using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using TagKid.Lib.Exceptions;

namespace TagKid.Lib.Utils
{
    public static class Validate
    {
        public static void StringLength(string key, string value, int maxLength, int minLength = 0, bool trim = true)
        {
            if (value == null)
            {
                if (minLength == -1) // allows null
                    return;
                throw new UserException("{0} cannot be empty", key);
            }

            if (trim)
                value = value.Trim();

            if (value.Length < minLength)
                throw new UserException("{0} cannot be less than {1} characters", key, minLength);

            if (value.Length > maxLength)
                throw new UserException("{0} cannot be more than {1} characters", key, maxLength);
        }

        public static string EncryptPwd(string pwd)
        {
            return BitConverter.ToString(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(pwd))).Replace("-", String.Empty);
        }

        public static void IsEmail(string email)
        {
            if (email == null)
                throw new UserException("Invalid email address!");
            var isEmail = Regex.IsMatch(email,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase);

            if (!isEmail)
                throw new UserException("{0} is not a valid email address!", email);
        }

        public static void Equals(string key, object value, object expectedValue)
        {
            if (value != expectedValue)
                throw new UserException("{0} must be {1}", key, expectedValue);
        }

        public static void Range<T>(string key, T value, T min, T max, bool gte = true, bool lte = true) where T : IComparable
        {
            if (gte && value.CompareTo(min) == -1)
                throw new UserException("{0} must be greater than or equal to {1}", key, min);
            if (!gte && value.CompareTo(min) != 1)
                throw new UserException("{0} must be greater than {1}", key, min);
            if (lte && value.CompareTo(max) == 1)
                throw new UserException("{0} must be less than or equal to {1}", key, max);
            if (!lte && value.CompareTo(max) != -1)
                throw new UserException("{0} must be less than {1}", key, max);
        }
    }
}
