
namespace Taga.Core.IoC
{
    public static class ServiceProvider
    {
        private static IServiceProvider _provider;

        public static IServiceProvider Provider
        {
            get
            {
                return _provider;
            }
            set
            {
                if (_provider == null)
                {
                    _provider = value;
                }
                throw new ServiceProviderAlreadySetException(_provider.GetType());
            }
        }
    }
}