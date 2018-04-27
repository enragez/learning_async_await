using System;
using System.Threading;
using Helpers;

namespace WorkerAsyncAPM.NET1
{
    internal class Worker
    {
        public bool WorkCompleted { get; set; }

        private delegate void LongOperationDelegate();

        public void DoWork()
        {
            Console.WriteLine("Начало работы");

            WorkCompleted = false;
            
            var longOperationDel = new LongOperationDelegate(LongOperation);

            longOperationDel.BeginInvoke(new AsyncCallback(WorkEndedCallback), longOperationDel);
        }

        private void WorkEndedCallback(IAsyncResult asyncResult)
        {
            var longOperationDel = (LongOperationDelegate)asyncResult.AsyncState;

            longOperationDel.EndInvoke(asyncResult);
            WorkCompleted = true;

            Console.WriteLine();
            Console.WriteLine("Работа завершена");
        }

        private void LongOperation()
        {
            Console.WriteLine("Работаю...");

            Thread.Sleep(Helper.WorkDelay);
        }
    }
}
