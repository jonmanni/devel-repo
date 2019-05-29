using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntToString
{
    class Program
    {
        static void Main(string[] args)
        {
            var devNumber = 2;
            var jono = "/device/fan" + devNumber.ToString();
            // also String.Format("{0},{1}", "123", 4);
            Console.WriteLine(jono);
            jono = jono + 2.ToString();
            Console.ReadKey();
        }
    }
}
