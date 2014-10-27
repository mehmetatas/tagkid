using Taga.Core.Repository;

namespace TagKid.Lib.Database
{
    public interface ITransactionalDb : IReadWriteDb, ITransactionalUnitOfWork
    {

    }
}