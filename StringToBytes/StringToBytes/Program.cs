using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StringToBytes
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            string kaksi = "255 45";
            byte[] tavutTaulukossa = { 0, 0 };
            byte[] resultBytes = { 0, 0 };
            var tavutStr = kaksi.Split();
            tavutTaulukossa[0] = Convert.ToByte(tavutStr[1]);
            tavutTaulukossa[1] = Convert.ToByte(tavutStr[0]);
            var resultOma = i2cAppCmd("read", "0xFE 0xA0", "1");
            Console.WriteLine(resultOma);
            var resultStr = resultOma.Split();
            resultBytes[0] = Convert.ToByte(resultStr[1]);
            resultBytes[1] = Convert.ToByte(resultStr[0]);
            foreach (var s in resultStr) Console.WriteLine(s);
            foreach (var b in resultBytes) Console.WriteLine(b);
            Console.ReadKey();
            */
            var time = 1000;
            byte[] READ_VIN = { 0x88 };
            byte[] READ_IOUT = { 0x8C };
            double mv = 27169;
            var bv = 0;
            var R = -1;
            double mi = 808;
            var bi = 20475;
            //var Ri = -1;

            var measVoltage = 46.8;
            //var readInputVoltageReg = I2C.Read(I2CAddress, 2, READ_VIN);
            byte[] readInputVoltageReg = new byte[] { };
            var readStr = i2cAppCmdRead("0x88", "2");
            if (readStr.StartsWith("error")||readStr.StartsWith("Unab"))
            {
                Console.WriteLine("I2C App read result: " + readStr + ", reading again...");
                Thread.Sleep(1000);
                readStr = i2cAppCmdRead("0x88", "2");
            }
            string[] readStrSplit = readStr.Split(' ');
            var str0 = readStrSplit[0];
            var str1 = readStrSplit[1];

            //double readVoltageValue;
            if (readStrSplit.Length > 1)
            {
                readInputVoltageReg[0] = (byte)Convert.ToInt32(str1, 16);
                readInputVoltageReg[1] = (byte)Convert.ToInt32(str0, 16);
                //readVoltageValue = (double)((readInputVoltageReg[1] << 8) + readInputVoltageReg[0]);
            }
            else
            {
                readInputVoltageReg[0] = (byte)Convert.ToInt32(readStrSplit[0], 16);
                //readVoltageValue = (double)((readInputVoltageReg[1] << 8) + readInputVoltageReg[0]);
            }

            //byte[] swapBytes = SwapBytes(readInputVoltageReg);
            var readVoltageValue = (double)((readInputVoltageReg[1] << 8) + readInputVoltageReg[0]);
            var Vin = (1 / mv) * ((readVoltageValue * Math.Pow(10, -R) - bv) * 60.5147);
            var result = Math.Abs(100 - Vin / measVoltage * 100);
            Console.WriteLine("DUT voltage divided by PSU voltage " + result + " %");
            
            Thread.Sleep(time);
            var measCurrent = 12.1;
            //var readOutputCurrentReg = I2C.Read(I2CAddress, 2, READ_IOUT);
            byte[] readOutputCurrentReg = new byte[] { };
            //swapBytes = SwapBytes(readOutputCurrentReg);
            var readCurrentValue = (double)((readOutputCurrentReg[1] << 8) + readOutputCurrentReg[0]);
            var Iout = (1 / mi) * (readCurrentValue * Math.Pow(10, -R) - bi);
            result = Math.Abs(100 - Iout / measCurrent * 100);
            Console.WriteLine("DUT current divided by PSU current " + result + " %");            
        }

        // i2cAppCmdRead("0x98","1")
        // i2cAppCmdRead("0xfe","2")
        public static string i2cAppCmdRead(string regAdress, string count)
        {
            const string path = "D:\\DATA\\i2cApp\\";
            const string appFileName = "i2cApp.exe";
            //const string read0x98 = " 0 100 read 0x73 0x98 1";
            string args = " 0 50 " + "read" + " 0x18 " + regAdress + " " + count;
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = path + appFileName,
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            proc.Start();
            //while (!proc.StandardOutput.EndOfStream)
            //{
            string line = proc.StandardOutput.ReadLine();
            //var strAsByte = Byte.Parse(line);
            //Console.WriteLine(strAsByte);
            string kaksi = "ff 7f";
            return kaksi;
            //}
        }
    }
}