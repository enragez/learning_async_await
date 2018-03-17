using System;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerAsyncCSharp5
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

            await Task.Delay(5000);
        }
    }
}