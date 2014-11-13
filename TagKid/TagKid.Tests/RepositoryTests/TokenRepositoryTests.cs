using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TagKid.Core.Database;
using TagKid.Core.Models.Database;
using TagKid.Core.Repositories;
using TagKid.Core.Utils;

namespace TagKid.Tests.RepositoryTests
{
    [TestClass]
    public class TokenRepositoryTests : BaseRepositoryTests
    {
        [TestMethod]
        public void Should_Create_Token()
        {
            var token = new Token
            {
                ExpireDate = DateTime.Now.AddDays(7).TrimMillis(),
                Guid = Util.GenerateGuid(),
                Type = TokenType.Auth,
                UseDate = DateTime.Now.TrimMillis(),
                UserId = 1
            };

            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<ITokenRepository>();
                repo.Save(token);
                db.Save();
            }

            Token token2;

            using (var db = Db.Readonly())
            {
                var repo = db.GetRepository<ITokenRepository>();
                token2 = repo.GetById(token.Id);
            }

            Assert.IsNotNull(token2);

            Assert.AreEqual(token.ExpireDate, token2.ExpireDate);
            Assert.AreEqual(token.Guid, token2.Guid);
            Assert.AreEqual(token.Id, token2.Id);
            Assert.AreEqual(token.Type, token2.Type);
            Assert.AreEqual(token.UseDate, token2.UseDate);
            Assert.AreEqual(token.UserId, token2.UserId);
        }
    }
}