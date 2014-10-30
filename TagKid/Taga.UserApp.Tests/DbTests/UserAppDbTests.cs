using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taga.UserApp.Tests.DbTests.EF;
using Taga.UserApp.Tests.DbTests.NH;

namespace Taga.UserApp.Tests.DbTests
{
    public abstract partial class UserAppDbTests
    {
        protected UserAppDbTests()
        {
            DbTestInitializer.Initialize(IsSqlServer);
            InitDb();
            TestCleanup();
        }

        protected abstract void InitDb();

        protected abstract bool IsSqlServer { get; }

        protected abstract bool IsEntityFramework { get; }

        [TestCleanup]
        public void TestCleanup()
        {
            DbTestInitializer.ClearDb(IsEntityFramework);
        }
    }
}
