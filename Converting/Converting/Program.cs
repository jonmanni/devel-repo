using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converting
{
    class Program
    {
        static void Main(string[] args)
        {
            double d1 = 9973;
            double d2 = -165;
            var result1 = DoubleToBytes(d1);
            var result2 = DoubleToBytes(d2);
            Console.WriteLine(ListToConsole(result1));
            Console.WriteLine(ListToConsole(result2));
            var arr1 = DoubleToByteArray(d1);
            var arr2 = DoubleToByteArray(d2);
            Console.WriteLine(ArrayToConsole(arr1));
            Console.WriteLine(ArrayToConsole(arr2));
            Array.Reverse(arr1);
            Array.Reverse(arr2);
            Console.WriteLine(ByteArrayToNumber(arr1));
            Console.WriteLine(ByteArrayToNumber(arr2));
            Console.ReadKey();
        }

        public static short ByteArrayToNumber(byte[] bytes)
        {
            var number = BitConverter.ToInt16(bytes, 0);
            return number;
        }

        public static byte[] DoubleToByteArray(double d)
        {
            var list = new List<byte>();
            foreach (var bl in BitConverter.GetBytes((short)d).Reverse())
            {
                list.Add(bl);
            }
            var bytes = new byte[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                byte ba = (byte)list[i];
                bytes[i] = ba;
            }
            return bytes;
        }

        public static string ArrayToConsole(byte[] list)
        {
            var str = "";
            foreach (var l in list)
            {
                str += l + " ";
            }
            return str;
        }

        public static List<byte> DoubleToBytes(double d)
        {
            var list = new List<byte>();
            foreach (var bl in BitConverter.GetBytes((short)d).Reverse())
            {
                list.Add(bl);
            }

            return list;
        }

        public static string ListToConsole(List<byte> list)
        {
            var str = "";
            foreach (var l in list)
            {
                str += l + " ";
            }
            return str;
        }
    }
}
