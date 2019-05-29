using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loop
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "";
            List<string> l = new List<string>();
            s.Contains("h");
            l.Contains("f");
            // luvut 100 ... 1
            for (int i = 0; i < 100; i++)
            {
                var j = 100;
                j = j - i;                
                Console.WriteLine(j);                
            }            
            Console.ReadKey();           
        }
    }
}
