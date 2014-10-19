using Ninject;
using System;

namespace Taga.Core.IoC
{
    public class ServiceProvider : IServiceProvider
    {
        public static IServiceProvider Provider = new ServiceProvider();

        private readonly IKernel _kernel = new StandardKernel();

        private ServiceProvider()
        {

        }

        public IServiceProvider Register(Type serviceType, Type classType, object singleton = null)
        {
            var bind = _kernel.Bind(serviceType);

            if (singleton == null)
                bind.To(classType);
            else
                bind.ToConstant(singleton);

            return this;
        }

        public object GetOrCreate(Type serviceType)
        {
            return _kernel.Get(serviceType);
        }
    }
}
