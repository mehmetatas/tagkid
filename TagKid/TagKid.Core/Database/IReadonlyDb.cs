using System;
using TagKid.Core.Repositories;

namespace TagKid.Core.Database
{
    public interface IReadonlyDb : IRepositoryProvider, IDisposable
    {
    }
}