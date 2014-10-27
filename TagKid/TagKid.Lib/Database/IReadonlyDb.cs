using System;
using TagKid.Lib.Repositories;

namespace TagKid.Lib.Database
{
    public interface IReadonlyDb : IDisposable, IRepositoryProvider
    {

    }
}