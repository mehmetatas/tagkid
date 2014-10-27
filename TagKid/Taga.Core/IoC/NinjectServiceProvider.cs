using System;
using Ninject;

namespace Taga.Core.IoC
{
    public class NinjectServiceProvider : IServiceProvider
    {
        private readonly IKernel _kernel = new StandardKernel();

        public IServiceProvider Register(Type serviceType, Type classType, object singleton = null)
        {
            var bind = _kernel.Bind(serviceType);

            if (singleton == null)
            {
                bind.To(classType);
            }
            else
            {
                if (singleton.GetType() != classType)
                {
                    throw new InvalidSingletonTypeException(classType, singleton.GetType());
                }

                bind.ToConstant(singleton);
            }

            return this;
        }

        public object GetOrCreate(Type serviceType)
        {
            return _kernel.Get(serviceType);
        }
    }
}
