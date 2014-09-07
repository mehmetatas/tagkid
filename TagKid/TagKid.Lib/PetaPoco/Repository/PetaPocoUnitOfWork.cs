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

        static PetaPocoUnitOfWork()
        {
            var mapper = new TagKidMapper();
            foreach (var type in mapper.Types)
                Mappers.Register(type, mapper);
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
