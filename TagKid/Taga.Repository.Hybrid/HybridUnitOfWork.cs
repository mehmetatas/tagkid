using System.Collections.Generic;
using System.Data;
using Taga.Core.IoC;
using Taga.Core.Repository;
using Taga.Core.Repository.Base;
using Taga.Repository.Hybrid.Commands;

namespace Taga.Repository.Hybrid
{
    public class HybridUnitOfWork : UnitOfWork, IHybridUnitOfWork
    {
        private static readonly IHybridDbProvider HybridDbProvider = ServiceProvider.Provider.GetOrCreate<IHybridDbProvider>();

        private readonly Queue<IHybridUowCommand> _commands = new Queue<IHybridUowCommand>();

        private IDbConnection _connection;
        private IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = HybridDbProvider.CreateConnection();
                }
                return _connection;
            }
        }

        protected override ITransaction OnBeginTransaction(IsolationLevel isolationLevel)
        {
            var tran = Connection.BeginTransaction(isolationLevel);
            return new HybridTransaction(tran);
        }

        protected override void OnSave()
        {
            while (_commands.Count > 0)
            {
                var cmd = _commands.Dequeue();
                cmd.Execute(Connection);
            }
        }

        protected override void OnDispose()
        {
            if (_connection == null)
            {
                return;
            }

            _connection.Close();
            _connection.Dispose();
            _connection = null;
        }

        void IHybridUnitOfWork.Insert(object entity)
        {
            _commands.Enqueue(new InsertCommand(entity));
        }

        void IHybridUnitOfWork.Update(object entity)
        {
            _commands.Enqueue(new UpdateCommand(entity));
        }

        void IHybridUnitOfWork.Delete(object entity)
        {
            _commands.Enqueue(new DeleteCommand(entity));
        }
    }
}
