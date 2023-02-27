using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConvertor.Interfaces
{
    public interface IStorage
    {
        public string ReadFileAsString(string path);

        public void Write(string content, string path);
    }
}