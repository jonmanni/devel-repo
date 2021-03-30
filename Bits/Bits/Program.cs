using System;
using System.Collections.Generic;

namespace Bits
{
    class Program
    {
        static void Main(string[] args)
        {
            var epsilon = Double.Epsilon;
            Console.WriteLine(1.0 - epsilon);
            var readBytes = new byte[] { 0x18, 0xA0 };
            var bytes16Bit = BitConverter.ToUInt16(readBytes, 0);
            Array.Reverse(readBytes);
            var bytes16BitReverse = BitConverter.ToUInt16(readBytes, 0);
            Console.WriteLine(ConvertToBase(10, 2, bytes16Bit.ToString()));

            var SlaveAddresses = new List<string>() { "0x4A", "0x4B", "0x4C" };
            bool[] resultArray = new bool[SlaveAddresses.Count];
            for(var i = 0; i < SlaveAddresses.Count; i++)
            {
                resultArray[i] = false;
            }
            resultArray[0] = true;
            Console.WriteLine(resultArray.Length);
            byte[] bytesClose = new byte[] { 0xD0, 0x18 };
            byte[] bytesOpen = new byte[] { 0xD0, 0x98 };
            byte[] bytesRevClose = new byte[] { 0xD0, 0x18 };
            byte[] bytesRevOpen = new byte[] { 0xD0, 0x98 };
            Array.Reverse(bytesRevOpen);
            Array.Reverse(bytesRevClose);
            var valueClose = BitConverter.ToUInt16(bytesClose, 0);
            var valueOpen = BitConverter.ToUInt16(bytesOpen, 0);
            var valueRevClose = BitConverter.ToUInt16(bytesRevClose, 0);
            var valueRevOpen = BitConverter.ToUInt16(bytesRevOpen, 0);
            //var valueBitsClose = BitConverter.ToUInt16(bytesClose, 0);
            //var valueBitsOpen = BitConverter.ToUInt16(bytesOpen, 0);

            Console.WriteLine("ADP Close [0]:{0} [1]:{1}", bytesClose[0], bytesClose[1]);
            IsBitSet16Bit(valueClose);
            Console.WriteLine("ADP Open [0]:{0} [1]:{1}", bytesOpen[0], bytesOpen[1]);
            IsBitSet16Bit(valueOpen);

            Console.WriteLine("Bytes reversed:");
            Console.WriteLine("ADP Close (Reversed) [0]:{0} [1]:{1}", bytesRevClose[0], bytesRevClose[1]);
            IsBitSet16Bit(valueRevClose);
            Console.WriteLine("ADP Open (Reversed) [0]:{0} [1]:{1}", bytesRevOpen[0], bytesRevOpen[1]);
            IsBitSet16Bit(valueRevOpen);
            Print8Bits(0xF0);
            //Print16Bits(0xF0);
            IsBitSet16Bit(0xF0);
            Console.ReadKey();
        }
        /// <summary>
        /// Converts number from base to another base
        /// </summary>
        /// <param name="fromBase">From base</param>
        /// <param name="toBase">To base (2, 8, 10 or 16)</param>
        /// <param name="n">Number to convert</param>
        /// <returns>Number (string) in toBase</returns>
        public static string ConvertToBase(int fromBase, int toBase, string n)
        {
            if (toBase != 2 && toBase != 8 && toBase != 10 && toBase != 16) return "CONVERSION FAILED!";
            return Convert.ToString(Convert.ToInt32(n, fromBase), toBase);
        }

        public static void Print8Bits(byte b)
        {
            for (var i = 0; i <= 7; i++)
            {
                Console.WriteLine("pos {0} - {1}", i, IsBitSet(b, i));
            }
        }

        public static void Print16Bits(byte b)
        {
            for (var i = 0; i <= 7; i++)
            {
                Console.WriteLine("pos {0} - {1}", i, IsBitSet16Bit(b));
            }
        }

        public static short IsBitSet16Bit(ushort wordVal)
        {
            //Int16 WordVal = 16;
            short bitVal = 0;
            for (var i = 0; i <= 15; i++)
            {
                bitVal = (short)((wordVal >> i) & 0x1);
                var sL = String.Format("Bit #{0:d} = {1:d}", i, bitVal);
                Console.WriteLine(sL);
                //if (i == 7) Console.WriteLine("----------");
            }

            return bitVal;
        }

        /// <summary>
        /// pos 0 is least significant bit, pos 7 is most.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static int IsBitSet(byte b, int pos)
        {
            if ((b & (1 << pos)) != 0) return 1;
            return 0;
        }
    }
}