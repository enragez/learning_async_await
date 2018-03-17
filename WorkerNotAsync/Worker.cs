using System;
using System.Threading;
using Helpers;

namespace WorkerNotAsync
{
    internal class Worker
    {
        public bool WorkCompleted { get; set; }
        
        public void DoWork()
        {
            WorkCompleted = false;
            
            Console.WriteLine("Начало работы");
            
            LongOperation();

            WorkCompleted = true;
            
            Console.WriteLine("Работа завершена");
        }

        private void LongOperation()
        {
            Console.WriteLine("Работаю...");

            Thread.Sleep(Helper.WorkDelay);
        }
    }
}