using Taga.Core.Repository;

namespace TagKid.Core.Database
{
    public interface IReadWriteDb : IReadonlyDb, IUnitOfWork
    {
    }
}