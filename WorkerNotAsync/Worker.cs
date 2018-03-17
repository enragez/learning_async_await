using System;
using System.Threading;

namespace WorkerNotAsync
{
    internal class Worker
    {
        public bool IsCompleted { get; set; }
        
        public void DoWork()
        {
            IsCompleted = false;
            
            Console.WriteLine("Начало работы");
            
            LongOperation();

            IsCompleted = true;
            
            Console.WriteLine("Работа завершена");
        }

        private void LongOperation()
        {
            Console.WriteLine("Работаю...");

            Thread.Sleep(5000);
        }
    }
}