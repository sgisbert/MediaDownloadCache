using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MediaDownloadCache
{
    public static class Helper
    {
        /// <summary>
        /// Download a file asynchronously in the provided path, 
        /// save it with the provided filename.
        /// </summary>
        public static void DownloadFileAsync(string url, string target)
        {
            using (var wc = new WebClient())
            {
                try
                {
                    //wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                    wc.DownloadFileCompleted += wc_DownloadFileCompleted;
                    wc.QueryString.Add("target", target);
                    wc.DownloadFileAsync(new Uri(url), target);
                }
                catch (Exception)
                {
                    DeleteFile(target);
                }
            }
        }

        /// <summary>
        /// Download a file synchronously in the provided path, 
        /// save it with the provided filename.
        /// </summary>
        public static void DownloadFileSync(string url, string target)
        {
            using (var wc = new WebClient())
            {
                try
                {
                    wc.DownloadFile(new Uri(url), target);
                }
                catch (Exception)
                {
                    DeleteFile(target);
                }
            }
        }

        //private static void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        //{
        //    // 50% | 5000 bytes out of 10000 bytes retrieven.
        //    Console.WriteLine(e.ProgressPercentage + "% | " + e.BytesReceived + " bytes");
        //}

        private static void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var target = ((System.Net.WebClient)(sender)).QueryString["target"];
            if (e.Cancelled)
            {
                // The download has been cancelled
                DeleteFile(target);
                return;
            }

            if (e.Error != null) // We have an error! Retry a few times, then abort.
            {
                DeleteFile(target);
                return;
            }
        }

        /// <summary>
        /// Get the filename from a web url 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetFilename(string url)
        {
            Uri uri = new Uri(url);
            return uri.Host + "_" + uri.AbsolutePath.Replace("/", "_");
            //return System.IO.Path.GetFileName(uri.LocalPath);
        }

        /// <summary>
        /// Gets a MemoryStream from a file 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static MemoryStream ReadFile(string path)
        {
            return new System.IO.MemoryStream(System.IO.File.ReadAllBytes(path));
        }

        /// <summary>
        /// Deletes the provided file 
        /// </summary>
        public static void DeleteFile(string path)
        {
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }
    }
}
