using Taga.Core.IoC;

namespace TagKid.Application.Bootstrapping
{
    public interface IBootstrapper
    {
        void Bootstrap(IServiceProvider prov);
    }
}