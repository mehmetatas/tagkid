using Taga.Core.Repository.Base;

namespace TagKid.Lib.PetaPoco.Repository
{
    public class PetaPocoUnitOfWork : UnitOfWorkBase
    {
        private readonly Database _db;

        internal Database Db
        {
            get { return _db; }
        }

        public PetaPocoUnitOfWork()
        {
            _db = new Database("tagkid");
            _db.BeginTransaction();
        }

        public override void Save()
        {
            _db.CompleteTransaction();
        }

        protected override void OnDispose()
        {
            _db.Dispose();
        }
    }
}
