using System;
using System.IO;

namespace MediaDownloadCache
{
    public class Download
    {
        public MemoryStream DownloadUrl(string url, int minutes, string path)
        {
            string filename = Helper.GetFilename(url);
            string target = path + "\\" + filename;
            System.IO.Directory.CreateDirectory(path);

            Console.WriteLine("file to save: " + filename);

            // File exists?
            if (System.IO.File.Exists(target))
            { // YES
                var file = Helper.ReadFile(target);
                DateTime lastModified = System.IO.File.GetLastWriteTime(target);

                Console.WriteLine("File exists");
                Console.WriteLine("Modified on " + lastModified);

                // Is it older than x minutes? Cache outdated
                if (DateTime.Now > lastModified.AddMinutes(minutes))
                {
                    Console.WriteLine("Cache outdated");
                    Helper.DownloadFileAsync(url, target);
                }

                // return current file
                return file;
            }
            else
            { // NO
                Console.WriteLine("Download File");

                // Download synchronously
                Helper.DownloadFileSync(url, target);
                return Helper.ReadFile(target);
            }
        }
    }
}
