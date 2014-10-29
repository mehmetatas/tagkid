using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taga.UserApp.Tests.DbTests.EF;
using Taga.UserApp.Tests.DbTests.NH;

namespace Taga.UserApp.Tests.DbTests
{
    public abstract partial class UserAppDbTests
    {
        protected UserAppDbTests()
        {
            DbTestInitializer.Initialize(!(this is NHMySqlTests));
            InitDb();
            TestCleanup();
        }

        protected abstract void InitDb();

        [TestCleanup]
        public void TestCleanup()
        {
            DbTestInitializer.ClearDb(this is EFSqlServerTests);
        }
    }
}
