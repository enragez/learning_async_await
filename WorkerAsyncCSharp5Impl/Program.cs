using System;
using System.Threading;

namespace WorkerAsyncTPL.NET45.Implementation
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var worker = new Worker();
            
            worker.DoWork();

            while (!worker.WorkCompleted)
            {
                Console.Write(".");
                Thread.Sleep(100);
            }

            Console.ReadLine();
        }
    }
}