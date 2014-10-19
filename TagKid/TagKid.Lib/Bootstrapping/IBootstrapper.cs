using Taga.Core.IoC;

namespace TagKid.Lib.Bootstrapping
{
    public interface IBootstrapper
    {
        void Bootstrap(IServiceProvider prov);
    }
}