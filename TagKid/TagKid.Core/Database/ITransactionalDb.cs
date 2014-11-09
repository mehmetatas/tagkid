using Taga.Core.Repository;

namespace TagKid.Core.Database
{
    public interface ITransactionalDb : IReadWriteDb, ITransactionalUnitOfWork
    {
    }
}