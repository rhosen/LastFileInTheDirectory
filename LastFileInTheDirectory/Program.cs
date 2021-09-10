using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace LastFileInTheDirectory
{
    class Program
    {
        static void Main(string[] args)
        {
            var directory = new DirectoryInfo(GetDirectoryPath());
            var file = GetLastFileInDirectory(directory);
            PrintLastFile(file, directory);
        }

        private static FileInfo GetLastFileInDirectory(DirectoryInfo directory)
        {
            var myFile = directory.GetFiles()
                .OrderByDescending(f => f.LastWriteTime)
                .FirstOrDefault();
            return myFile;
        }

        static string GetDirectoryPath()
        {
            var configurationBuilder = new ConfigurationBuilder();
            string appSettingPath = Path.Combine(Directory.GetCurrentDirectory(), "appSetting.json");
            configurationBuilder.AddJsonFile(appSettingPath, false);
            var directoryPath = configurationBuilder.Build().GetSection("Path").Value;
            return directoryPath;
        }

        private static void PrintLastFile(FileInfo file, DirectoryInfo directory)
        {
            string contents = File.ReadAllText(file.FullName);
            Console.WriteLine($"Printing text from the last file of the directory: [{directory}]");
            Console.WriteLine("================================================= \n");
            Console.WriteLine(contents);
            Console.ReadLine();
        }
    }
}
