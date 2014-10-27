using System.Collections.Generic;

namespace TagKid.Rest
{
    public class UserService : IUserService
    {
        public User GetUser(long id)
        {
            return new User { Id = 1, Name = "User 1" };
        }

        public IList<User> GetUsers(string name)
        {
            return new[]
            {
                new User {Id = 1, Name = "User 1"},
                new User {Id = 2, Name = "User 2"}
            };
        }

        public long SaveUser(User user)
        {
            return 3;
        }

        public void DeleteUser(long id)
        {
            
        }
    }
}