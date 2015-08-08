using System;
using System.IO;

namespace NotLimited.Framework.Common.Logging
{
    public class FileSystemLogWriter : ILogWriter, IDisposable
    {
        private readonly StreamWriter _writer;

        public FileSystemLogWriter(string path, bool append = true)
        {
            _writer = new StreamWriter(path, append);
        }

        public void WriteLine(string text)
        {
            _writer.WriteLine(text);
            _writer.Flush();
        }

        public void Write(string text)
        {
            _writer.Write(text);
            _writer.Flush();
        }

	    public void Dispose()
	    {
		    Dispose(true);
			GC.SuppressFinalize(this);
	    }

	    protected virtual void Dispose(bool disposing)
	    {
		    if (disposing)
		    {
			    _writer.Dispose();
		    }
	    }
    }
}