using FileConvertor.Storage;
using static System.Net.Mime.MediaTypeNames;
using Shouldly;

namespace MoraviaTests
{
    [TestClass]
    public class StorageTest
    {
        [TestMethod]
        public void ReadTest()
        {
            var fileSystemStorage = new FileSystemStorage();

            var content = fileSystemStorage.ReadFileAsString(@"TestFiles\input.xml");

            content.ShouldNotBeEmpty();
        }


        [TestMethod]
        public void WriteTest()
        {
            var fileSystemStorage = new FileSystemStorage();

            var content = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<root>
  <title>Title1</title>
  <text>Text1</text>
</root>
";

            fileSystemStorage.Write(content, path: @"TestFiles\output.xml");

            content.ShouldNotBeEmpty();
        }
    }
}