using MediaDownloadCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup parameters

            // Cache folder destination
            string location = System.Reflection.Assembly.GetExecutingAssembly().Location;

            // Cache folder name
            var directory = System.IO.Path.GetDirectoryName(location) + "\\cache";

            // Time in minutes to keep the local file in cache
            var timeSpan = 1;

            // File to download
            string url = "https://raw.githubusercontent.com/sgisbert/MediaDownloadCache/master/Tests/test.jpg";

            // Get the file
            Download mdc = new Download();
            var stream = mdc.DownloadUrl(url, timeSpan, directory);

            Console.WriteLine("Read " + stream.Length + " bytes");
            Console.WriteLine("Press enter...");
            Console.ReadLine();
        }
    }
}
