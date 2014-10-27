using System.Collections.Generic;

namespace TagKid.Rest
{
    public interface IUserService
    {
        User GetUser(long id);

        IList<User> GetUsers(string name);

        long SaveUser(User user);

        void DeleteUser(long id);
    }
}