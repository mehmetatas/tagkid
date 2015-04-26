
namespace TagKid.Framework.Exceptions
{
    public class Errors
    {
        public static readonly Error Unknown = new Error(-1, "Error_Unknown");
        
        public static readonly Error F_RouteResolvingError = new Error(1, "F_RouteResolvingError");
    }
}
