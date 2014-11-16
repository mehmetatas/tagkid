using System;
using TagKid.Core.Repository;

namespace TagKid.Core.Database
{
    public interface IReadonlyDb : IRepositoryProvider, IDisposable
    {
    }
}