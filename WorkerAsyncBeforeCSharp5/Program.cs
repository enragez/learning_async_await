using System;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerAsyncBeforeCSharp5
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var worker = new Worker();
            
            Task.Factory.StartNew(() => { worker.DoWork(); });
            
            while (!worker.WorkCompleted)
            {
                Console.Write(".");
                Thread.Sleep(100);
            }

            Console.ReadLine();
        }
    }
}