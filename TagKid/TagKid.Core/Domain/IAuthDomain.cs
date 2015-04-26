
namespace TagKid.Core.Domain
{
    public interface IAuthDomain
    {
        void Signup(string fullname, string email, string username, string password);
    }
}
