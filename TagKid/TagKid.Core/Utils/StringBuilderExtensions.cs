using System.Text;

namespace TagKid.Core.Utils
{
    internal static class StringBuilderExtensions
    {
        public static bool BufferEquals(this StringBuilder builder, string str)
        {
            if (builder.Length != str.Length)
            {
                return false;
            }

            for (var i = 0; i < str.Length; i++)
            {
                if (str[i] != builder[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static int LastIndexOf(this StringBuilder builder, char c)
        {
            for (var i = builder.Length - 1; i >= 0; i--)
            {
                if (builder[i] == c)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}