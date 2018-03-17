using System;
using System.Threading.Tasks;
using Helpers;

namespace WorkerAsyncTPL.NET45
{
    internal class Worker
    {
        public bool WorkCompleted { get; set; }
        
        public async void DoWork()
        {
            WorkCompleted = false;
            
            Console.WriteLine("Начало работы");
            
            await LongOperation();

            WorkCompleted = true;
            
            Console.WriteLine();
            Console.WriteLine("Работа завершена");
        }

        private async Task LongOperation()
        {
            Console.WriteLine("Работаю...");

            await Task.Delay(Helper.WorkDelay);
        }
    }
}