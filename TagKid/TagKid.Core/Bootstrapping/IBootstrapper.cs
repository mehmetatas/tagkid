using TagKid.Framework.IoC;

namespace TagKid.Core.Bootstrapping
{
    public interface IBootstrapper
    {
        void Bootstrap(IDependencyContainer container);
    }
}