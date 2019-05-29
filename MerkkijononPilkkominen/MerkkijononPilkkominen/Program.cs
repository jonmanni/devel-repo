using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerkkijononPilkkominen
{
    class Program
    {        
        static void Main(string[] args)
        {
            //tulostaisi 2.23  3  4  5  0  9  5
            double[] luvut = ErotaLuvut("2.23 3 4 5 k      9 ;5");
            Console.WriteLine(String.Join(" ", luvut));

            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }

        public static double[] ErotaLuvut(string table)
        {
            if (table.Length == 0) return new double[] { 0 };
            table = table.Trim();
            var strings = table.Split(new char[] { ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);
            var strLength = strings.Length;
            var resultTable = new double[strLength];
            foreach(var s in strings)
            {
                Console.WriteLine("|{0}|", s);
            }
            
            for (int i = 0; i < strLength; i++)
            {
                if (String.IsNullOrEmpty(strings[i])) continue;
                if (!double.TryParse(strings[i], out resultTable[i])) continue;
                //{
                //    resultTable[i] = Convert.ToDouble(strings[i]);
                //}
                
            }
            return resultTable;
        }
    }
}
