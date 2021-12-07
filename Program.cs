using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace AdobeConnectRecord
{
    internal class Program
    {
        private const string FileName = "adobeconnect.exe";
        private const string ModeNormal = "pbMode=normal";
        private const string ModeOffline = "pbMode=offline";

        internal static void Main(string[] args)
        {
            // Concat all arguments with a single space character
            var allArgs = string.Join(" ", args);

            if (!string.IsNullOrWhiteSpace(allArgs) && allArgs.Contains(ModeNormal))
            {
                Console.Write("Use DOWNLOAD Mode? [Yes|No]");

                // Check user answer
                var answer = Console.ReadLine().ToLower();
                if (answer == "yes" || answer == "y")
                    allArgs = allArgs.Replace(ModeNormal, ModeOffline);
            }

            // Make full path to FileName
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), FileName);

            // Check Before Execute
            if (!File.Exists(path))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"File \"{FileName}\" not found!");
                Console.ReadKey(true);
            }

            // Yay :)
            Process.Start(path, allArgs);
        }
    }
}
