
namespace TagKid.Core.Domain
{
    public interface IDomainServiceProvider
    {
        TService GetService<TService>() where TService : ITagKidDomainService;
    }
}
