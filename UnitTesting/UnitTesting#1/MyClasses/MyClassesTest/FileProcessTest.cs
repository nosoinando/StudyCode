using MyClasses;

namespace MyClassesTest
{
    [TestClass]
    public class FileProcessTest
    {
        protected string _GoodFileName;
        private const string BAD_FILE_NAME = @"C:\Windows\Olabb.exe";
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
        public void FileNameDoesExist()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            SetGoodFileName();

            TestContext.WriteLine($"Checking File {_GoodFileName}");

            fromCall = fp.FileExists(_GoodFileName);

            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        public void FileNameDoesNotExist()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            TestContext.WriteLine($"Checking File {BAD_FILE_NAME}");

            fromCall = fp.FileExists(BAD_FILE_NAME);

            Assert.IsFalse(fromCall);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileNameNullOrEmpty_UsingAttribute()
        {
            FileProcess fp = new FileProcess();

            TestContext.WriteLine("Checking for a null file");

            fp.FileExists("");

        }

        [TestMethod]
        public void FileNameNullOrEmpty_UsingTryCatch()
        {
            FileProcess fp = new FileProcess();

            TestContext.WriteLine("Checking for a null file");

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