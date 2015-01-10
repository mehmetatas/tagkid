using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taga.Core.IoC;
using Taga.Core.Repository;
using TagKid.Application.Bootstrapping;

namespace TagKid.Tests.RepositoryTests
{
    public class BaseRepositoryTests
    {
        static BaseRepositoryTests()
        {
            Bootstrapper.StartApp();
        }

        public BaseRepositoryTests()
        {
            TruncateAll();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            TruncateAll();
        }

        public static void TruncateAll()
        {
            using (var uow = ServiceProvider.Provider.GetOrCreate<IUnitOfWork>())
            {
                var repo = ServiceProvider.Provider.GetOrCreate<ISqlRepository>();
                repo.ExecSp("TruncateAll");
                uow.Save();
            }
        }
    }
}
