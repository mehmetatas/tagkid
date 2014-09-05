using System;

namespace Taga.Core.IoC
{
    public interface IServiceProvider
    {
        void Register(Type interfaceType, Type classType, object singleton = null);

        void Register<TInterface, TClass>(TClass singleton = null)
            where TClass : class, TInterface, new();

        TInterface GetOrCreate<TInterface>();
    }
}
