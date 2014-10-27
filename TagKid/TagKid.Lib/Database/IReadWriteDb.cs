using Taga.Core.Repository;

namespace TagKid.Lib.Database
{
    public interface IReadWriteDb : IReadonlyDb, IUnitOfWork
    {

    }
}