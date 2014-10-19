using System;
using System.Security.Cryptography;
using System.Text;

namespace TagKid.Lib.Utils
{
    public static class Util
    {
        public static string EncryptPwd(string pwd)
        {
            using (var sha1 = SHA1.Create())
            {
                return BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(pwd))).Replace("-", String.Empty);
            }
        }
    }
}