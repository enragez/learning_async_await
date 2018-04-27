using System;
using System.Threading;
using Helpers;

namespace WorkerAsyncTPL.NET4
{
    internal class Worker
    {
        public bool WorkCompleted { get; set; }
        
        public void DoWork()
        {
            Console.WriteLine("Начало работы");
            
            WorkCompleted = false;
            
            LongOperation();

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