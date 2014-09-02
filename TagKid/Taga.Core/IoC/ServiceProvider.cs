using System;
using System.Collections.Generic;
using Taga.Core.DynamicProxy;

namespace Taga.Core.IoC
{
    public class ServiceProvider : IServiceProvider
    {
        public static IServiceProvider Provider = new ServiceProvider();

        private readonly Dictionary<Type, Func<object>> _services = new Dictionary<Type, Func<object>>();

        private ServiceProvider()
        {
            RegisterSingleton(NullCallHandler.Instance);
        }

        public void Register<TInterface, TClass>() where TClass : class, TInterface
        {
            var interfaceType = typeof(TInterface);
            var classType = typeof(TClass);

            Func<object> func = () => Activator.CreateInstance(classType);

            Register(interfaceType, classType, func);
        }

        public void RegisterProxy<TInterface, TClass>(ICallHandler callHandler = null) where TClass : class, TInterface
        {
            var interfaceType = typeof(TInterface);
            var classType = typeof(TClass);

            if (callHandler == null)
                callHandler = GetOrCreate<ICallHandler>();

            //Func<object> func = () => Proxy.Of<TClass>(callHandler);
            Func<object> func = () => Activator.CreateInstance(classType);

            Register(interfaceType, classType, func);
        }

        public void RegisterSingleton<TInterface>(TInterface singleton)
        {
            if (singleton == null)
                throw new ArgumentNullException("singleton");

            var interfaceType = typeof(TInterface);
            var classType = singleton.GetType();

            Func<object> func = () => singleton;

            Register(interfaceType, classType, func);
        }

        private void Register(Type interfaceType, Type classType, Func<object> func)
        {
            if (!interfaceType.IsInterface)
                throw new InvalidOperationException(String.Format("{0} must be an interface type", interfaceType));

            if (!classType.IsClass)
                throw new InvalidOperationException(String.Format("{0} must be a class type", classType));

            if (_services.ContainsKey(interfaceType))
                _services[interfaceType] = func;
            else
                _services.Add(interfaceType, func);
        }

        public TInterface GetOrCreate<TInterface>()
        {
            var interfaceType = typeof(TInterface);
            if (_services.ContainsKey(interfaceType))
                return (TInterface)_services[interfaceType]();
            throw new InvalidOperationException(String.Format("No implementations registered for {0}", interfaceType));
        }
    }
}
