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

        public static Guid GenerateGuid()
        {
            return Guid.NewGuid();
        }

        [Obsolete("Convert this method to use AutoMapper")]
        public static void MapTo<TBase, TDerived>(TBase baseObj, TDerived derived)
            where TBase : class, new()
            where TDerived : TBase, new()
        {
            if (baseObj == null)
            {
                return;
            }

            var props = baseObj.GetType().GetProperties();
            foreach (var prop in props)
            {
                prop.SetValue(derived, prop.GetValue(baseObj));
            }
        }
    }
}