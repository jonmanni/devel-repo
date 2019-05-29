using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzz
{
    class Program
    {
        /*
         * "Write a program that prints the numbers from 1 to 100. 
         * But for multiples of three print “Fizz” instead of the 
         * number and for the multiples of five print “Buzz”. For 
         * numbers which are multiples of both three and five 
         * print “FizzBuzz”."
        */
        static void Main(string[] args)
        {
            for (int i = 1; i <= 100; i++)
            {
                Console.WriteLine(Check(i));
            }
            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }

        public static string Check(int l)
        {
            var result = "" + l;
            var threes = "Fizz";
            var fives = "Buzz";
            if (l % 3 == 0) result = threes;
            if (l % 5 == 0) result = fives;
            if (l % 3 == 0 && l % 5 == 0) result = threes + fives;
            return result;
        }
    }
}
