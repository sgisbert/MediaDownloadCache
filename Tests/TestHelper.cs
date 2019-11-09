using MediaDownloadCache;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            url = "https://www.pixelstalk.net/wp-content/uploads/2016/09/Free-Download-3D-Wallpaper-For-Laptop-Desktop.jpg";

            string filename = Helper.GetFilename(url);

            var localpath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            location = System.IO.Path.GetDirectoryName(localpath) + "\\cache";
            System.IO.Directory.CreateDirectory(location);
            target = location + "\\" + filename;

        }

        [ClassCleanup]
        public static void ClassCleanup()
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

        public void DownloadFileSync()
        {
            Helper.DownloadFileSync(url, target);
        }

        public void ReadFile()
        {
            Helper.ReadFile(target);
        }
    }
}
