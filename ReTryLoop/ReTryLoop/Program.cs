using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReTryLoop
{
    class Program
    {
        readonly static int number = 1;
        static void Main(string[] args)
        {
            var retries = 1;
            var timeOut = 500;

            bool result;
            do
            {
                Console.WriteLine("Lets loop!");
                result = CheckState(timeOut);
                //retries--;
            } while (retries-- > 0);
            Console.WriteLine("The End");
            Console.ReadKey();
        }
        public static bool CheckState(int timeOut)
        {
            int[] table = { 0, 1, 2, 3 };
            foreach (var t in table)
            {
                Console.WriteLine("Try to find correct number... {0}", t);
                Thread.Sleep(timeOut);
                if (t == number) return true;
            }
            return false;
        }
    }
}
