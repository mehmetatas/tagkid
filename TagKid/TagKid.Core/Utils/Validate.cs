using System.Text.RegularExpressions;
using TagKid.Core.Exceptions;

namespace TagKid.Core.Utils
{
    public static class Validate
    {
        public static void IsEmail(string email)
        {
            if (email == null)
                E.T(ErrorCodes.Validation_InvalidEmailAddress, "Email adress is null");
            var isEmail = Regex.IsMatch(email,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase);

            if (!isEmail)
                E.T(ErrorCodes.Validation_InvalidEmailAddress, "Email address is invalid");
        }
    }
}