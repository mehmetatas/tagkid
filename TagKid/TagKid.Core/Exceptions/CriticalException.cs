
namespace TagKid.Core.Exceptions
{
    public class CriticalException : TagKidException
    {
        public CriticalException(Error error, string message = null, params object[] args)
            : base(error, message, args)
        {
        }
    }
}
