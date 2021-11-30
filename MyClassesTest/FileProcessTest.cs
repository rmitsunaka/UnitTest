using Microsoft.VisualStudio.TestTools.UnitTesting;  //Restore nuget packages
using MyClasses;
using System;
using System.IO;

namespace MyClassesTest
{
    [TestClass]
    public class FileProcessTest : TestBase
    {
        protected string _GoodFileName;
        private const string BAD_FILE_NAME = @"C:\Windows\Xablau.exe";

        public TestContext TestContext { get; set; }

        protected void SetGoodFileName()
        {
            _GoodFileName = TestContext.Properties["GoodFileName"].ToString();
            if (_GoodFileName.Contains("[AppPath]"))
            {
                _GoodFileName = _GoodFileName.Replace("[AppPath]",
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }
        }

        [TestMethod]
        public void FileNameDoesExists()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            SetGoodFileName();

            //Criando um _GoodFileName
            if (!string.IsNullOrEmpty(_GoodFileName))
                File.AppendAllText(_GoodFileName,"Qualquer informação");

            TestContext.WriteLine(@"Checking file " + _GoodFileName);

            fromCall = fp.FileExists(@"C:\Windows\Regedit.exe");

            //Deletando _GoodFileName
            if (File.Exists(_GoodFileName))
                File.Delete(_GoodFileName);

            Assert.IsTrue(fromCall);                
        }

        [TestMethod]
        public void FileNameDoesNotExists()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            TestContext.WriteLine(@"Checking file " + BAD_FILE_NAME);

            fromCall = fp.FileExists(@"C:\Windows\Xablau.exe");

            Assert.IsFalse(fromCall);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileNameNullorEmpty_UsingAttribute()
        {
            FileProcess fp = new FileProcess();

            fp.FileExists("");
        }

        [TestMethod]
        public void FileNameNullorEmpty_UsingTryCatch()
        {
            FileProcess fp = new FileProcess();
            try
            {
                fp.FileExists("");
            }
            catch(ArgumentNullException)
            {
                return;
            }
            Assert.Fail("Call to FileExists() did NOT throw an ArgumentNullException");
        }
    }
}
