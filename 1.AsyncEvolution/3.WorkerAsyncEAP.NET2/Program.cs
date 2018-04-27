using System;
using System.ComponentModel;
using System.Threading;

namespace WorkerAsyncEAP.NET2
{
    internal class Program
    {
        private static bool _workCompleted;
        
        public static void Main(string[] args)
        {
            var worker = new Worker();
            worker.WorkCompleted += OnWorkCompleted;
            
            worker.DoWork();

            while (!_workCompleted)
            {
                Console.Write(".");
                Thread.Sleep(100);
            }

            Console.ReadLine();
        }

        private static void OnWorkCompleted(object sender, AsyncCompletedEventArgs args)
        {
            _workCompleted = true;
        }
    }
}