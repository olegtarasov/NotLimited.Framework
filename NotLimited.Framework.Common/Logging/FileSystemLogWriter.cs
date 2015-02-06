using System.IO;

namespace NotLimited.Framework.Common.Logging
{
    public class FileSystemLogWriter : ILogWriter
    {
        private readonly StreamWriter _writer;

        public FileSystemLogWriter(string path)
        {
            _writer = new StreamWriter(path, true);
        }

        public void WriteLine(string text)
        {
            _writer.WriteLine(text);
            _writer.Flush();
        }

        public async void Write(string text)
        {
            _writer.Write(text);
            _writer.Flush();
        }
    }
}