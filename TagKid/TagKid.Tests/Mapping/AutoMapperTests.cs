using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TagKid.Tests.Mapping
{
    [TestClass]
    public class AutoMapperTests
    {
        [TestMethod]
        public void TestEnum()
        {
            Mapper.CreateMap<UserDTO, User>().ForMember(entity => entity.Type,
                cfg => cfg.MapFrom(dto => default(UserType)));

            var source = new UserDTO { Username = "Test" };
            var target = Mapper.Map<User>(source);

            Assert.AreEqual("Test", target.Username);
            Assert.AreEqual(0, target.Type);
        }
    }

    public enum UserType
    {
        User,
        Admin,
        Moderator
    }

    public class UserDTO
    {
        public string Username { get; set; }
    }

    public class User
    {
        public string Username { get; set; }

        public int Type { get; set; }
    }
}
