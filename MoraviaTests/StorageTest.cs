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
            // arrange
            var fileSystemStorage = new FileSystemStorage();

            // act
            var content = fileSystemStorage.ReadFileAsString(@"TestFiles\input.xml");

            // assert
            content.ShouldNotBeEmpty();
        }

        [TestMethod]
        public void WriteTest()
        {
            // arrange
            var fileSystemStorage = new FileSystemStorage();
            var content = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<root>
  <title>Title1</title>
  <text>Text1</text>
</root>
";

            // act
            fileSystemStorage.Write(content, path: @"TestFiles\output.xml");

            // assert
            content.ShouldNotBeEmpty();
        }
    }
}