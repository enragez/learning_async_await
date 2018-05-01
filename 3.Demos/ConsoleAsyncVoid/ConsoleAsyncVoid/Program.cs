using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAsyncVoid
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync();

            //MainAsync2();
        }

        #region 1
        private async static Task MainAsync()
        {
            Console.WriteLine("Started");

            try
            {
                DelayAndThrowAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception occured: " + ex.Message);
            }
            Console.WriteLine("Finished");
        }

        private async static void DelayAndThrowAsync()
        {
            await Task.Delay(100);
            throw new InvalidOperationException();
        }
        #endregion

        #region 2
        private async static Task MainAsync2()
        {
            Console.WriteLine("Started");

            try
            {
                Foo(() => { throw new InvalidOperationException(); });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured: " + ex.Message);
            }
            Console.WriteLine("Finished");
        }

        private static void Foo(Action action)
        {
            action();
        }
        #endregion 2
    }
}
