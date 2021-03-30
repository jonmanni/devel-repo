using System;
using System.IO;
using System.IO.Compression;

namespace ZipTestingNETCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var condition = false;
            var str = "nll";

            if (condition)
            {
                if (str.Contains("null"))
                {
                    Console.WriteLine("error!");
                    throw new ApplicationException("app_except");
                }                
                Console.WriteLine("Doing the Deed...");
            }
            else
            {
                Console.WriteLine("ERROR!");
                throw new ApplicationException("APP_EXCEPT");
            }
            Console.ReadKey();

            double frequency_kHz = 3650000;
            Console.WriteLine($"Freq in kHz: {frequency_kHz}, freq in MHz: {frequency_kHz / 1e3}");

            var pathToZip = @"C:\DATA\";
            var zipFileName = "AEQE_IA4203697747.zip";  // firstEntry = "calibration_masteramber_antennanetwork_ant0.json"
            //var zipFileName = "AEQE_IA4203155935.zip";  // firstEntry = "AEQE_IA4203155935/"

            var zipFilePath = string.Concat(pathToZip, zipFileName);

            using (ZipArchive zip = ZipFile.Open(zipFilePath, ZipArchiveMode.Read))
            {
                var firstEntry = zip.Entries[0].ToString();
                if(firstEntry.EndsWith(Path.AltDirectorySeparatorChar.ToString(), StringComparison.Ordinal))
                {
                    Console.WriteLine("File structure of {0} is OK!", zipFileName);
                    //ZipFile.ExtractToDirectory(zipFileName, zipFilePath);
                }

                if (firstEntry.EndsWith(".json"))
                {
                    Console.WriteLine("Wrong structure in {0}", zipFileName);
                    Console.WriteLine("No folder named \\{0} found!", Path.GetFileNameWithoutExtension(zipFileName));
                    pathToZip += Path.GetFileNameWithoutExtension(zipFileName);
                    zipFilePath = string.Concat(pathToZip, zipFileName);
                    
                    //ZipFile.ExtractToDirectory(zipFileName, zipFileName);
                }

                Console.WriteLine(zip.Entries[0]);
                Console.WriteLine("*******");
                foreach (var entry in zip.Entries)
                {
                    Console.WriteLine(entry);
                }
            }

            //ZipFile.CreateFromDirectory();
            Console.ReadKey();
        }
    }
}
