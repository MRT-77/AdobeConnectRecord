using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace AdobeConnectRecord
{
    internal class Program
    {
        private const string FileName = "adobeconnect.exe";
        private const string ModeNormal = "pbMode=normal";
        private const string ModeOffline = "pbMode=offline";

        private static string NewLine => Environment.NewLine;

        internal static void Main(string[] args)
        {
            var localDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // Concat all arguments with a single space character
            var allArgs = string.Join(" ", args);

            if (!string.IsNullOrWhiteSpace(allArgs))
            {
#if DEBUG
                // Write arguments in log file
                File.AppendAllText(Path.Combine(localDir, "log.txt"),
                    $"{DateTime.Now:s}{NewLine}{allArgs}{NewLine}{NewLine}",
                    Encoding.UTF8);
#endif

                if (allArgs.Contains(ModeNormal))
                {
                    Console.Write("Use Record Mode? [yes/No] ");

                    // Check user answer
                    var answer = Console.ReadLine().ToLower();
                    if (answer == "yes" || answer == "y")
                        allArgs = allArgs.Replace(ModeNormal, ModeOffline);
                }
            }

            // Make full path to FileName
            var path = Path.Combine(localDir, FileName);

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
