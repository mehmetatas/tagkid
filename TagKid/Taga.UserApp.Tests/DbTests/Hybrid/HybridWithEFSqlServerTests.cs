using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taga.Core.IoC;
using Taga.Core.Repository;
using Taga.Repository.Hybrid;
using Taga.UserApp.Core.Database.EF;

namespace Taga.UserApp.Tests.DbTests.Hybrid
{
    [TestClass]
    public class HybridWithEFSqlServerTests : UserAppDbTests
    {
        protected override void InitDb()
        {
            var prov = ServiceProvider.Provider;

            prov.RegisterSingleton<IHybridDbProvider>(new HybridSqlServerProvider());
            prov.Register<IQueryProvider, UserAppEFQueryProvider>();
            prov.Register<IUnitOfWork, HybridUnitOfWork>();
            prov.Register<ITransactionalUnitOfWork, HybridUnitOfWork>();
            prov.Register<IRepository, HybridRepository>();
        }

        protected override bool IsSqlServer
        {
            get { return true; }
        }

        protected override bool IsEntityFramework
        {
            get { return true; }
        }
    }
}
