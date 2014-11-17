using System;
using System.Security.Cryptography;
using System.Text;

namespace TagKid.Core.Utils
{
    public static class Util
    {
        public static string EncryptPwd(string pwd)
        {
            using (var sha256 = SHA256.Create())
            {
                return BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(pwd))).Replace("-", String.Empty);
            }
        }

        public static string GenerateConfirmationCode()
        {
            using (var sha256 = SHA256.Create())
            {
                return BitConverter.ToString(sha256.ComputeHash(Guid.NewGuid().ToByteArray())).Replace("-", String.Empty);
            }
        }

        public static string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}