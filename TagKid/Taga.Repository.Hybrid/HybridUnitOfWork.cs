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
        private readonly Queue<IHybridUowCommand> _commands = new Queue<IHybridUowCommand>();

        private IDbConnection _connection;
        private IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    var hybridDbProvider = ServiceProvider.Provider.GetOrCreate<IHybridDbProvider>();
                    _connection = hybridDbProvider.CreateConnection();
                    _connection.Open();
                }
                return _connection;
            }
        }

        private IQueryProvider _queryProvider;
        IQueryProvider IHybridUnitOfWork.QueryProvider
        {
            get
            {
                if (_queryProvider == null)
                {
                    _queryProvider = ServiceProvider.Provider.GetOrCreate<IQueryProvider>();
                    _queryProvider.SetConnection(Connection);
                }
                return _queryProvider;
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
                var dbCmd = Connection.CreateCommand();

                if (Transaction != null)
                {
                    dbCmd.Transaction = ((HybridTransaction) Transaction).DbTransaction;
                }

                cmd.Execute(dbCmd);
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
