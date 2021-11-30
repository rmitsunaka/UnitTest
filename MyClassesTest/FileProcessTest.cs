using Microsoft.VisualStudio.TestTools.UnitTesting;  //Restore nuget packages
using MyClasses;
using System;

namespace MyClassesTest
{
    [TestClass]
    public class FileProcessTest
    {
        private const string BAD_FILE_NAME = @"C:\Windows\Xablau.exe";
        private const string GOOD_FILE_NAME = @"C:\Windows\Regedit.exe";

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void FileNameDoesExists()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            TestContext.WriteLine(@"Checking file " + GOOD_FILE_NAME);

            fromCall = fp.FileExists(@"C:\Windows\Regedit.exe");

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
                fp.FileExists("File");
            }
            catch
            {
                return;
            }
            Assert.Fail("Call to FileExists() did NOT throw an ArgumentNullException");
        }
    }
}
