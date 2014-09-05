using System;
using System.Collections.Generic;

namespace Taga.Core.IoC
{
    public class ServiceProvider : IServiceProvider
    {
        public static IServiceProvider Provider = new ServiceProvider();

        private readonly Dictionary<Type, ServiceInfo> _services = new Dictionary<Type, ServiceInfo>();

        private ServiceProvider()
        {

        }

        public void Register(Type interfaceType, Type classType, object singleton = null)
        {
            var info = new ServiceInfo
            {
                SingletonInstance = singleton,
                ServiceType = classType
            };

            if (_services.ContainsKey(interfaceType))
                _services[interfaceType] = info;
            else
                _services.Add(interfaceType, info);
        }

        public void Register<TInterface, TClass>(TClass singleton = null) where TClass : class, TInterface, new()
        {
            Register(typeof(TInterface), typeof(TClass), singleton);
        }

        public TInterface GetOrCreate<TInterface>()
        {
            var interfaceType = typeof(TInterface);

            Type genericArgType = null;
            if (!_services.ContainsKey(interfaceType))
            {
                if (interfaceType.IsGenericType)
                {
                    genericArgType = interfaceType.GetGenericArguments()[0];
                    interfaceType = interfaceType.GetGenericTypeDefinition();
                }

                if (!_services.ContainsKey(interfaceType))
                    throw new InvalidOperationException("No service registered for: " + interfaceType);
            }

            var info = _services[interfaceType];

            if  (info.IsSingleton)
                return (TInterface)info.SingletonInstance;

            if (genericArgType == null)
                return (TInterface)Activator.CreateInstance(info.ServiceType);

            var genericType = info.ServiceType.MakeGenericType(genericArgType);
            return (TInterface)Activator.CreateInstance(genericType);
        }
    }

    public class ServiceInfo
    {
        public Type ServiceType { get; set; }
        public object SingletonInstance { get; set; }

        public bool IsSingleton
        {
            get { return SingletonInstance != null; }
        }
    }
}
