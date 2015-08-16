
namespace TagKid.Core.Domain
{
    public interface IAuthDomain
    {
        void Register(string fullname, string email, string username, string password);
    }
}
