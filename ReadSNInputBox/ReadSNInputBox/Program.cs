using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadSNInputBox
{
    class Program
    {
        public enum AdpChipModel
        {
            Adp1048 = 1048,
            Adp1051 = 1051,
            Adp1055 = 1055
        }

        public enum EControlState
        {
            RadioSw,
            TestabilitySw
        }

        public enum ECoreAgentName
        {
            MemoryAccess,
            PeripheralDevice,
            LedDevice,
            FanDevice,
            EacDevice,
            RfSynthesizer,
            DebugControl,
            UnitInfo,
            ThermometerDevice,
            TxGainControl,
            AntennaPath,
            ResetControl,
            LnaDevice,
            ConfigAccess,
            AmplifierDevice,
            GpioDevice,
            CpriControl,
            PsuMeasurements,
            CpriPort,
            AntennaLinePort,
            RxGainControl,
            VswrControl,
            FaultControl,
            BlackBox
        }

        static void Main(string[] args)
        {
            var ParamFile = "c:\\config\\PSU_4rd_proto.hex";
            Console.WriteLine("Paramfile: {0}", ParamFile);
            //Console.WriteLine(ParamFile.Replace(".hex", string.Empty));
            var fileLength = ParamFile.Length;
            Console.WriteLine("Paramfile char count: {0}", fileLength);
            var slashChar = 0;
            for (int i = fileLength; i > 0; i--)
            {
                if (ParamFile.Substring(i - 1, 1).Equals("\\"))
                {
                    slashChar = i;
                    break;
                }
            }

            Console.WriteLine("Filename: {0}", ParamFile.Substring(slashChar));
            Console.WriteLine("GetFilename: {0}", GetFilename(ParamFile));

            byte b = 0xA0;
            Console.WriteLine("Byte :{0}    Byte shifted: {1}", b, b << 8);

            for (var retries = 0;; retries++)
            {
                Console.WriteLine(retries);
                //Needs to be written twice
                if (retries > 3)
                    throw new ApplicationException("Could not unlock eeprom within retry limits!");
            }

            AdpChipModel ChipModel = AdpChipModel.Adp1055;
            if (!ChipModel.Equals(AdpChipModel.Adp1048)) Console.WriteLine("Chip model is not 1048");
            var response = "ret /CpriPort/opt/port2 get cpri_state ==> done ok B\r\n\0";
            Console.WriteLine("[" + response + "]");
            if (response.Contains("\r\n\0")) response = response.Replace("\r\n\0", string.Empty);
            Console.WriteLine("[" + response + "]");
            uint[] _analogTrimRegister = {0xFE, 0x17};
            uint[] _cs2ReadRegister = {0x8C};
            uint[] _empty = { };
            Console.WriteLine("CS2 Trim, 2 pcs (" + _analogTrimRegister + "): ");
            Console.WriteLine("CS2 Trim, 2 pcs (" + TableToLog(_analogTrimRegister) + "): ");
            Console.WriteLine("CS2 Trim, 1 pcs (" + _cs2ReadRegister + "): ");
            Console.WriteLine("CS2 Trim, 1 pcs (" + TableToLog(_cs2ReadRegister) + "): ");
            Console.WriteLine("CS2 Trim, 0 pcs (" + _empty + "): ");
            Console.WriteLine("CS2 Trim, 0 pcs (" + TableToLog(_empty) + "): ");

            var agentName = ECoreAgentName.LnaDevice;
            var deviceName = "/rf/rx5";
            var state = EControlState.TestabilitySw;

            var path = CreateAgentPath("Core", agentName.ToString(), deviceName);

            var command = path + " set control_state ";
            command += state == EControlState.TestabilitySw ? "TESTABILITY_SW" : "RADIO_SW";
            Console.WriteLine("command: " + command);
            uint[] register = {0, 1, 2};

            if (register.Length > 3)
                throw new ArgumentException("Register size cannot be more than 3: size is " + register.Length);
            var size = Convert.ToInt32(Math.Pow(2, register.Length)) / 2;
            var dataToWrite = new uint[3];
            register.CopyTo(dataToWrite, 0);

            for (int i = 0; i < 1; i++)
            {
                Console.WriteLine("[" + i + "]");
            }

            int port = 2;
            var device = "/opt/port" + port;
            for (int i = 0; i < 5; i++)
            {
                if (i == 0) Console.WriteLine("EKA ON TÄMÄ: {0}", i);
                Console.WriteLine(i);
            }


            var input = Microsoft.VisualBasic.Interaction.InputBox("Read additional serial number", "Input window", "",
                0, 0);
            if (input.Equals(""))
            {
                Console.WriteLine("Serial number was empty!");
                throw new ApplicationException("EMPTY SERIAL_NUMBER");
            }

            var len = input.Length;
            if (input.Length != 10)
            {
                Console.WriteLine("Serial number length must be 10!");
                throw new ApplicationException("SIZE OF SERIAL_NUMBER MUST BE 10");
            }

            var serialNumber = input.ToUpper();
            Console.WriteLine("Serial number is: " + serialNumber);
            Console.WriteLine("ADP chip model: " + ChipModel);
            Console.ReadKey();
        }

        public static string GetFilename(string filepath)
        {
            var stringLength = filepath.Length;
            for (int i = stringLength; i > 0; i--)
            {
                if (filepath.Substring(i - 1, 1).Equals("\\"))
                {
                    var slashChar = i;
                    return filepath.Substring(slashChar);
                }
            }

            return "<FileNameNotFound>";
        }

        public static string TableToLog(IEnumerable<uint> table)
        {
            var resultString = " ";
            foreach (var t in table)
            {
                resultString += t + " ";
            }

            return resultString;
        }

        public static string CreateAgentPath(string dispatcher, string agentName, string devicePath)
        {
            // TODO:: dispatcher not used - check this! Will be needed..
            var path = "/" + agentName + devicePath;

            string response;
            var command = "/ action create " + agentName + " " + path + " " + devicePath;

            //var frmonResponse = DutSocket.SendData(command, out response);

            //if (EFrmonSuccess.Ok != frmonResponse && !response.Contains("0x6d") && !response.Contains("0x09"))
            //{               // path already taken error:  FRM58.03.R02-  0x6d           FRM58.03.R03+  0x09
            //    throw new ApplicationException("Failed to create agent: " + path + " response: " + response);
            //}

            return command;
        }
    }
}