using System;
using System.Collections.Generic;

namespace EkaVaan
{
    class Program
    {
        static void Main(string[] args)
        {
            string hexa1 = ":0200D700FFFF00";
            string hexa2 = ":02FA000012C000";
            string hexa3 = ":01A00500B500";
            var messageList = new List<byte>();
            //messageList.Add(215);
            for (int i = 0; i < 2; i++)
            {
                //messageList.Add(255);
            }
            messageList.Add(Convert.ToByte(hexa1.Substring(1, 2)));
            //messageList.ForEach(Console.WriteLine);
            foreach (var m in messageList)
            {
                Console.WriteLine(m);
            }
            //Console.WriteLine(messageList);
            Console.WriteLine("Tere mualima");
            //byte[] varInHex = messageList.SelectMany(BitConverter.GetBytes).ToArray();
            byte[] varInHex = messageList.ToArray();
            foreach (var v in varInHex)
            {
                Console.WriteLine(v);
            }
            Console.WriteLine("----------------------------------");
            writeHexString(hexa1);
            Console.WriteLine("----------------------------------2");
            writeHexString(hexa2);
            Console.WriteLine("----------------------------------2");
            writeHexString(hexa3);
            Console.ReadKey();

        }
        public static void writeHexString(string hexLine)
        {
            var messageList = new List<byte>();

            var dataLength = Convert.ToByte(hexLine.Substring(1, 2), 16);
            var addressMSB = Convert.ToByte(hexLine.Substring(3, 2), 16);
            var addressLSB = Convert.ToByte(hexLine.Substring(5, 2), 16);
            var dataMSB = Convert.ToByte(hexLine.Substring(9, 2), 16);
            var dataLSB = Convert.ToByte(hexLine.Substring(11, 2), 16);

            if (addressMSB != 0) messageList.Add(addressMSB);
            messageList.Add(addressLSB);

            messageList.Add(dataMSB);
            if (dataLength == 2) messageList.Add(dataLSB);

            byte[] varInHex = messageList.ToArray();
            foreach (var v in varInHex)
            {
                Console.WriteLine(v);
            }
        }
        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }
 }

