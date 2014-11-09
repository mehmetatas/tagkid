using Taga.Core.Repository;

namespace TagKid.Core.Database
{
    class ReadWriteDb : ReadonlyDb, IReadWriteDb
    {
        public ReadWriteDb(ITransactionalUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public virtual void Save()
        {
            UnitOfWork.Save();
        }
    }
}