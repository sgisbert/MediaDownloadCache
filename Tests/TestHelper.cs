using MediaDownloadCache;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        static string location;
        static string url;
        static string target;

        [ClassInitialize]
        public static void ClassInitialized(TestContext context)
        {
            url = "https://raw.githubusercontent.com/sgisbert/MediaDownloadCache/master/Tests/test.jpg";

            string filename = Helper.GetFilename(url);

            var localpath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            location = System.IO.Path.GetDirectoryName(localpath) + "\\cache";
            target = location + "\\" + filename;

        }

        [TestInitialize]
        public void TestInitialize()
        {
            System.IO.Directory.CreateDirectory(location);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            System.IO.Directory.Delete(location, true);
        }

        [TestMethod]
        public void GetFileName()
        {
            string filename = Helper.GetFilename(url);
            Assert.IsNotNull(filename);
            Assert.IsTrue(filename.Length > 0);
        }

        [TestMethod]
        public void DownloadFileSync()
        {
            Helper.DownloadFileSync(url, target);

            Assert.IsTrue(System.IO.File.Exists(target));

            var stream = Helper.ReadFile(target);
            Assert.IsNotNull(stream);
            Assert.AreEqual(stream.Length, 347566);
        }

        [TestMethod]
        public void ReadFile()
        {
            Helper.DownloadFileSync(url, target);

            var stream = Helper.ReadFile(target);
            Assert.IsNotNull(stream);
            Assert.AreEqual(stream.Length, 347566);
        }

        [TestMethod]
        public void Download()
        {
            Stopwatch timer = new Stopwatch();
            Download mdc = new Download();
            timer.Start();

            // First downloads
            MemoryStream stream = mdc.DownloadUrl(url, 10, location);
            Assert.IsNotNull(stream);
            Assert.AreEqual(stream.Length, 347566);
            Console.WriteLine("Download 1.1: " + timer.Elapsed);

            // First download, not blocked
            var url2 = "https://www.pixelstalk.net/wp-content/uploads/2016/09/Free-Download-3D-Wallpaper-For-Laptop-Desktop.jpg";
            stream = mdc.DownloadUrl(url2, 0, location);
            Assert.IsNotNull(stream);
            Assert.AreEqual(stream.Length, 347566);
            Console.WriteLine("Download 1.2: " + timer.Elapsed);
            Thread.Sleep(3000);

            // Second download cached
            stream = mdc.DownloadUrl(url, 60, location);
            Assert.IsNotNull(stream);
            Assert.AreEqual(stream.Length, 347566);
            Console.WriteLine("Download 2: CACHED " + timer.Elapsed);
            Thread.Sleep(3000);

            // Third download not cached
            stream = mdc.DownloadUrl(url, 0, location);
            Assert.IsNotNull(stream);
            Assert.AreEqual(stream.Length, 347566);
            Console.WriteLine("Download 3: NOT CACHED " + timer.Elapsed);

            // Third download, not blocked
            stream = mdc.DownloadUrl(url2, 60, location);
            Assert.IsNotNull(stream);
            Assert.AreEqual(stream.Length, 347566);
            Console.WriteLine("Download 4: NOT BLOCKED" + timer.Elapsed);

            Thread.Sleep(2000);

        }
    }
}
