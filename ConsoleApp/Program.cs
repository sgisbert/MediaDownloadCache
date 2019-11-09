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
            string location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(location) + "\\cache";
            var timeSpan = 1;

            //string url = "https://images.unsplash.com/photo-1498036882173-b41c28a8ba34";
            string url = "https://www.pixelstalk.net/wp-content/uploads/2016/09/Free-Download-3D-Wallpaper-For-Laptop-Desktop.jpg";

            Download mdc = new Download();
            var stream = mdc.DownloadUrl(url, timeSpan, directory);

            Console.WriteLine("Read " + stream.Length + " bytes");
            

            Console.WriteLine("Press enter...");
            Console.ReadLine();
        }
    }
}
