using System;
using System.ComponentModel;
using System.Threading;
using Helpers;

namespace WorkerAsyncEAP.NET2
{
    public delegate void WorkCompletedEventHandler(object sender, AsyncCompletedEventArgs args);

    internal class Worker
    {
        private delegate void WorkerEventHandler(AsyncOperation asyncOp);

        private readonly SendOrPostCallback _onCompletedDelegate;
        
        public event WorkCompletedEventHandler WorkCompleted;

        public Worker()
        {
            _onCompletedDelegate = new SendOrPostCallback(CompletedDelegateFunc);
        }
        
        private void CompletedDelegateFunc(object operationState)
        {
            AsyncCompletedEventArgs e = operationState as AsyncCompletedEventArgs;

            if (WorkCompleted != null)
            {
                WorkCompleted(this, e);
            }
        }

        public void DoWork(object userState)
        {
            AsyncOperation asyncOp = AsyncOperationManager.CreateOperation(userState);
            
            Console.WriteLine("Начало работы");
            WorkerEventHandler workInternal = new WorkerEventHandler(LongOperation);
            workInternal.BeginInvoke(asyncOp, null, null);
        }
        
        private void LongOperation(AsyncOperation asyncOp)
        {
            Console.WriteLine("Работаю...");
            Thread.Sleep(Helper.WorkDelay);

            AsyncCompletedEventArgs e = new AsyncCompletedEventArgs(null, false, asyncOp.UserSuppliedState);
            
            Console.WriteLine();
            Console.WriteLine("Работа завершена");
            
            asyncOp.PostOperationCompleted(_onCompletedDelegate, e);
        }
    }
}