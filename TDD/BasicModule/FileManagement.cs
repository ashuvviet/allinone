using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BasicModule
{
    public class FileManagement : IDisposable
    {
        public void WriteFileStats(string filePath, string outFilePath)
        {
            var fileContent = File.ReadAllText(filePath, Encoding.UTF8);
            var fileBytes = new FileInfo(filePath).Length;
            var fileWords = Regex.Matches(fileContent, @"\s+").Count + 1;
            var fileLines = Regex.Matches(fileContent, Environment.NewLine).Count + 1;
            var fileStats = $"{fileLines} {fileWords} {fileBytes}";
            File.AppendAllText(outFilePath, fileStats);
        }

        public FileStream CreateFile(string filePath)
        {
           using var file = File.Create(filePath);
           return file;
        }

        public long WriteFile(string filePath)
        {
            using var fileContent = File.OpenWrite(filePath);
            using (StreamWriter writetext = new StreamWriter(fileContent))
            {
                writetext.WriteLine("writing in text file");
            }

            return new FileInfo(filePath).Length;
        }

        public void Dispose()
        {
            
        }
    }
}
