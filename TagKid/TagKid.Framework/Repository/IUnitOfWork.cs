using System;

namespace TagKid.Framework.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();

        void Commit();

        void Rollback();
    }
}