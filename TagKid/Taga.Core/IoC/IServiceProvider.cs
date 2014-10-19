using System;

namespace Taga.Core.IoC
{
    public interface IServiceProvider
    {
        IServiceProvider Register(Type serviceType, Type classType, object singleton = null);
        
        object GetOrCreate(Type serviceType);
    }

    public static class ServiceProviderExtensions
    {
        public static void Register<TInterface, TClass>(this IServiceProvider prov, TClass singleton = null)
            where TClass : class, TInterface
        {
            prov.Register(typeof(TInterface), typeof(TClass), singleton);
        }

        public static TInterface GetOrCreate<TInterface>(this IServiceProvider prov)
        {
            return (TInterface)prov.GetOrCreate(typeof(TInterface));
        }
    }
}
