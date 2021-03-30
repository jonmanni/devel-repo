using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreLoops
{
    class Program
    {
        static void Main(string[] args)
        {
            var i = 0;
            var IsOk = false;
            var loopCount = 5;
            var fixtureOk = 4;
            // Do it atleast once
            if (loopCount <= 0) loopCount = 1;
            while (!IsOk && i < loopCount)
            {
                i++;
                Console.WriteLine("{0}. kerta", i);
                if (i == fixtureOk)
                {
                    Console.WriteLine("FIXTURE OK!");
                    IsOk = true;
                }
            }            
            Console.ReadKey();
        }
    }
}
