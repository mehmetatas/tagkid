using System;
using System.Collections.Generic;
using Taga.Core.Context;

namespace Taga.Core.Repository.Base
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        protected UnitOfWork()
        {
            Push(this);
        }

        public abstract void Save();

        void IDisposable.Dispose()
        {
            Pop();
            OnDispose();
        }

        protected virtual void OnDispose()
        {
        }

        #region Stack

        private static Stack<IUnitOfWork> Stack
        {
            get
            {
                var uowStack = CallContext.Get<Stack<IUnitOfWork>>("UnitOfWorkStack");
                if (uowStack == null)
                {
                    uowStack = new Stack<IUnitOfWork>();
                    CallContext.Set("UnitOfWorkStack", uowStack);
                }
                return uowStack;
            }
        }

        public static IUnitOfWork Current
        {
            get
            {
                if (Stack.Count == 0)
                    throw new InvalidOperationException("No UnitOfWork is available!");
                return Stack.Peek();
            }
        }

        private static void Push(IUnitOfWork uow)
        {
            Stack.Push(uow);
        }

        private static void Pop()
        {
            Stack.Pop();
        }

        #endregion
    }
}