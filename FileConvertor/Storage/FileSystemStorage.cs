using FileConvertor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConvertor.Storage
{
    public class FileSystemStorage : IStorage
    {
        public string ReadFileAsString(string path)
        {
            using (FileStream sourceStream = File.Open(path, FileMode.Open))
            {
                using (var reader = new StreamReader(sourceStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public void Write(string content, string path)
        {
            using (var targetStream = File.Open(path, FileMode.Create, FileAccess.Write))
            {
                using (var sw = new StreamWriter(targetStream))
                {
                    sw.Write(content);
                }
            }
        }
    }
}