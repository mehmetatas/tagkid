using Taga.Core.DynamicProxy;

namespace Taga.Core.IoC
{
    public interface IServiceProvider
    {
        void Register<TInterface, TClass>()
            where TClass : class, TInterface;

        void RegisterProxy<TInterface, TClass>(ICallHandler callHandler = null)
            where TClass : class, TInterface;

        void RegisterSingleton<TInterface>(TInterface singleton);

        TInterface GetOrCreate<TInterface>();
    }
}
