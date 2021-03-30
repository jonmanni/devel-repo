using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleMeasurements
{
    class Program
    {
        static void Main(string[] args)
        {
            // to get result: 15.93 and 26.72
            var sqStr1 = "14,15 26,72";
            var sqStr2 = "15,93 18,66";

            var firstValues = new List<Double>();
            var secondValues = new List<Double>();
            var maxValues = new List<Double>();
            Console.WriteLine(sqStr1);
            Console.WriteLine(sqStr2);
            var sqArray1 = sqStr1.Split(' ');
            var sqArray2 = sqStr2.Split(' ');
            firstValues.Add(double.Parse(sqArray1[0]));
            firstValues.Add(double.Parse(sqArray2[0]));
            secondValues.Add(double.Parse(sqArray1[1]));
            secondValues.Add(double.Parse(sqArray2[1]));
            maxValues.Add(firstValues.Max());
            maxValues.Add(secondValues.Max());
            Console.WriteLine(ListToConsole(maxValues));
            Console.WriteLine();
            Console.ReadKey();
        }

        public static string ListToConsole(List<double> list)
        {
            var str = "";
            foreach (var l in list)
            {
                str += l + " ";
            }
            return str;
        }

        public static List<double> ArrayToList(string[] array)
        {
            var list = new List<double>();
            foreach (var l in array)
            {
                list.Add(double.Parse(l));
            }
            return list;
        }
    }
}
