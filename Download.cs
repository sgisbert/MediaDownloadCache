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
             
            // File exists?
            if (System.IO.File.Exists(target))
            { // YES
                var file = Helper.ReadFile(target);
                DateTime lastModified = System.IO.File.GetLastWriteTime(target);

                // Is it older than x minutes? Cache outdated
                if (DateTime.Now > lastModified.AddMinutes(minutes))
                {
                    // update async for next request
                    Helper.DownloadFileAsync(url, target);
                }
                // return current file
                return file;
            }
            else
            { // NO
                // Download synchronously
                Helper.DownloadFileSync(url, target);
                return Helper.ReadFile(target);
            }
        }
    }
}
