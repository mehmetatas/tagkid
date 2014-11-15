using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TagKid.Core.Database;
using TagKid.Core.Models.Database;
using TagKid.Core.Repositories;
using TagKid.Core.Utils;

namespace TagKid.Tests.RepositoryTests
{
    [TestClass]
    public class ConfirmationCodeRepositoryTests : BaseRepositoryTests
    {
        [TestMethod, TestCategory("RepositoryTests")]
        public void Should_Create_Confirmation_Code()
        {
            var confirmCode = new ConfirmationCode
            {
                Code = Util.GenerateConfirmationCode(),
                ExpireDate = DateTime.Now.Date.AddDays(7),
                SendDate = DateTime.Now.Date,
                Reason = ConfirmationReason.NewUser,
                Status = ConfirmationCodeStatus.AwaitingConfirmation,
                UserId = 1,
                ConfirmDate = DateTime.Now.Date
            };

            using (var db = Db.ReadWrite())
            {
                var repo = db.GetRepository<IConfirmationCodeRepository>();

                repo.Save(confirmCode);
                db.Save();
            }

            ConfirmationCode code2;
            using (var db = Db.Readonly())
            {
                var repo = db.GetRepository<IConfirmationCodeRepository>();

                code2 = repo.GetById(confirmCode.Id);
            }

            Assert.IsNotNull(code2);

            Assert.AreEqual(confirmCode.Code, code2.Code);
            Assert.AreEqual(confirmCode.ConfirmDate, code2.ConfirmDate);
            Assert.AreEqual(confirmCode.ExpireDate, code2.ExpireDate);
            Assert.AreEqual(confirmCode.Id, code2.Id);
            Assert.AreEqual(confirmCode.Reason, code2.Reason);
            Assert.AreEqual(confirmCode.SendDate, code2.SendDate);
            Assert.AreEqual(confirmCode.Status, code2.Status);
            Assert.AreEqual(confirmCode.UserId, code2.UserId);
        }
    }
}