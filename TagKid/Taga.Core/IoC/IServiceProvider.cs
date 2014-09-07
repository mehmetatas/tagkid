using System;

namespace Taga.Core.IoC
{
    public interface IServiceProvider
    {
        void Register(Type interfaceType, Type classType, object singleton = null);

        TInterface GetOrCreate<TInterface>();
    }

    public static class ServiceProviderExtensions 
    {
        public static void Register<TInterface, TClass>(this IServiceProvider prov, TClass singleton = null)
            where TClass : class, TInterface, new()
        {
            prov.Register(typeof(TInterface), typeof(TClass), singleton);
        }
    }
}
