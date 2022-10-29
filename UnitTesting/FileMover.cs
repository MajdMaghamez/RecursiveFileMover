using System.IO;
using System.Text;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTesting
{
    [TestClass]
    public class FileMover
    {
        private static readonly string _testFile = "test.txt";

        [TestMethod]
        public void FileMover_hasWriteAccess_CreatedFileExists()
        {
            string source = ConfigurationManager.AppSettings["source"];
            if (!Directory.Exists(source)) Directory.CreateDirectory(source);

            string completeURL = Path.Combine(source, _testFile);

            if (File.Exists(completeURL))
                File.Delete(completeURL);

            using (FileStream fs = File.Create(completeURL))
            {
                byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.");
                fs.Write(info, 0, info.Length);
            }

            Assert.IsTrue(File.Exists(completeURL));
        }

        [TestMethod]
        public void FileMover_movingFile_MovedFileExists()
        {
            string source = ConfigurationManager.AppSettings["source"];
            if (!Directory.Exists(source)) Directory.CreateDirectory(source);

            string target = ConfigurationManager.AppSettings["target"];
            if (!Directory.Exists(target)) Directory.CreateDirectory(target);

            string completeSourceURL = Path.Combine(source, _testFile);
            string completeTargetURL = Path.Combine(target, _testFile);

            if (File.Exists(completeTargetURL))
                File.Delete(completeTargetURL);

            File.Move(completeSourceURL, completeTargetURL);

            Assert.IsTrue(File.Exists(completeTargetURL));
        }
    }
}
