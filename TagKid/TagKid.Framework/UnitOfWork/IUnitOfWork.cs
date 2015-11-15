using System;
using System.Data;

namespace TagKid.Framework.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

        void Rollback();

        void Commit();
    }
}
