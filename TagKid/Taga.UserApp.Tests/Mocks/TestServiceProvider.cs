using Ninject;
using Taga.IoC.Ninject;

namespace Taga.UserApp.Tests.Mocks
{
    class TestServiceProvider : NinjectServiceProvider
    {
        public void Reset()
        {
            Kernel = new StandardKernel();
        }
    }
}
