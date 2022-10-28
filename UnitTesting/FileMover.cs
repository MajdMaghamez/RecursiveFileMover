using System.IO;
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

            string completeURL = Path.Combine(source, _testFile);

            if (File.Exists(completeURL))
                File.Delete(completeURL);


            File.Create(completeURL);

            Assert.IsTrue(File.Exists(completeURL));
        }

        [TestMethod]
        public void FileMover_movingFile_MovedFileExists()
        {
            string source = ConfigurationManager.AppSettings["source"];
            string target = ConfigurationManager.AppSettings["target"];

            string completeSourceURL = Path.Combine(source, _testFile);
            string completeTargetURL = Path.Combine(target, _testFile);

            if (File.Exists(completeTargetURL))
                File.Delete(completeTargetURL);

            
            File.Move(completeSourceURL, completeTargetURL);

            Assert.IsTrue(File.Exists(completeTargetURL));
        }
    }
}
