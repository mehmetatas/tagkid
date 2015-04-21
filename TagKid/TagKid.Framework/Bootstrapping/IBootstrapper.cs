using TagKid.Framework.IoC;

namespace TagKid.Framework.Bootstrapping
{
    public interface IBootstrapper
    {
        void Bootstrap(IDependencyContainer container);
    }
}