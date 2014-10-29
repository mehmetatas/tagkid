using System.Data;

namespace Taga.Repository.Hybrid.Commands
{
    interface IHybridUowCommand
    {
        void Execute(IDbConnection conn);
    }
}