using System.Collections.Generic;
using DummyOrm.Sql.Command;

namespace TagKid.Framework.UnitOfWork
{
    public interface IAdoRepository
    {
        IList<T> ExecuteQuery<T>(Command cmd) where T : class, new();

        int ExecuteNonQuery(Command cmd);

        object ExecuteScalar(Command cmd);
    }
}