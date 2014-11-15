using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TagKid.Core.Database;
using TagKid.Core.Models.Database;
using TagKid.Core.Repositories;

namespace TagKid.Tests.RepositoryTests
{
    [TestClass]
    public class LoginRespositoryTests : BaseRepositoryTests
    {
        [TestMethod, TestCategory("RepositoryTests")]
        public void Should_Get_LastSuccessful_Login()
        {
            var login1 = new Login
            {
                Date = DateTime.Now.Date,
                Email = "mail@mail.com",
                FacebookId = "123456",
                Result = LoginResult.Successful,
                UserId = 2,
                Type = LoginType.Cookie,
                Username = "username"
            };
            
            var login2 = new Login
            {
                Date = DateTime.Now.Date,
                Email = "mail@mail.com",
                FacebookId = "123456",
                Result = LoginResult.ExpiredCookieToken,
                UserId = 2,
                Type = LoginType.Cookie,
                Username = "username"
            };

            var login3 = new Login
            {
                Date = DateTime.Now.Date,
                Email = "mail@mail.com",
                FacebookId = "123456",
                Result = LoginResult.Successful,
                UserId = 2,
                Type = LoginType.Cookie,
                Username = "username"
            };

            var login4 = new Login
            {
                Date = DateTime.Now.Date,
                Email = "mail@mail.com",
                FacebookId = "123456",
                Result = LoginResult.Successful,
                UserId = 3,
                Type = LoginType.Cookie,
                Username = "username"
            };

            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<ILoginRepository>();
                
                repo.Save(login1);
                repo.Save(login2);
                repo.Save(login3);
                repo.Save(login4);

                db.Save();
            }

            Login lastSuccessfulLogin;
            using (var db = Db.Readonly())
            {
                var repo = db.GetRepository<ILoginRepository>();

                lastSuccessfulLogin = repo.GetLastSuccessfulLogin(2);
            }

            Assert.IsNotNull(lastSuccessfulLogin);
            Assert.AreEqual(login3.Date, lastSuccessfulLogin.Date);
            Assert.AreEqual(login3.Email, lastSuccessfulLogin.Email);
            Assert.AreEqual(login3.FacebookId, lastSuccessfulLogin.FacebookId);
            Assert.AreEqual(login3.Id, lastSuccessfulLogin.Id);
            Assert.AreEqual(login3.Result, lastSuccessfulLogin.Result);
            Assert.AreEqual(login3.Type, lastSuccessfulLogin.Type);
            Assert.AreEqual(login3.UserId, lastSuccessfulLogin.UserId);
            Assert.AreEqual(login3.Username, lastSuccessfulLogin.Username);
        }
    }
}
