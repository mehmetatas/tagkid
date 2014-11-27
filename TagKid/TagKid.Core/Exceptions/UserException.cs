
namespace TagKid.Core.Exceptions
{
    public class UserException : TagKidException
    {
        public UserException(Error error, string message = null, params object[] args)
            : base(error, message, args)
        {
        }
    }
}
