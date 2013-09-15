using System;
using System.Linq;
using System.Text;

namespace NotLimited.Framework.Common.Logging
{
	public enum LogLevel { Debug, Info, Warn, Error, Catastrophe }

	public class Logger
	{
		private static readonly StringBuilder _cache;
		private static ILogWriter _writer;

		public static LogLevel Level { get; set; }

		private readonly string _typeName;

		static Logger()
		{
			_cache = new StringBuilder();
			Level = LogLevel.Info;
		}

		public static void Init(ILogWriter writer)
		{
			_writer = writer;

			string cache = _cache.ToString();
			if (!string.IsNullOrEmpty(cache))
				_writer.Write(cache);
		}

		private Logger(string typeName)
		{
			_typeName = typeName;
		}

		public static Logger Get(Type type)
		{
			return new Logger(type.Name);
		}

		public static Logger Get<T>()
		{
			return new Logger(typeof(T).Name);
		}

		private static void Write(string message)
		{
			if (_writer == null)
				_cache.AppendLine(message);
			else
				_writer.WriteLine(message);
		}

		public void Write(string message, LogLevel level, Exception e)
		{
			if (level >= Level)
				Write(string.Format("{0} [{1}] {2}: {3}. {4}", DateTime.Now.ToString("HH:mm:ss"), level.ToString(), _typeName, message, e != null ? e.ToString() : ""));
		}

		public void WriteFormat(string message, LogLevel level, params object[] args)
		{
			if (level >= Level)
				Write(string.Format("{0} [{1}] {2}: {3}", DateTime.Now.ToString("HH:mm:ss"), level.ToString(), _typeName, string.Format(message, args)));
		}

		public void WriteEval(string message, LogLevel level, params Func<string>[] args)
		{
			if (level >= Level)
			{
				Write(string.Format("{0} [{1}] {2}: {3}", DateTime.Now.ToString("HH:mm:ss"), level.ToString(), _typeName, string.Format(message, args.Select(x => x()).ToArray())));
			}
		}

		public void Debug(string message, Exception e = null)
		{
			Write(message, LogLevel.Debug, e);
		}

		public void DebugFormat(string message, params object[] args)
		{
			WriteFormat(message, LogLevel.Debug, args);
		}

		public void DebugEval(string message, params Func<string>[] args)
		{
			WriteEval(message, LogLevel.Debug, args);
		}

		public void Info(string message, Exception e = null)
		{
			Write(message, LogLevel.Info, e);
		}

		public void InfoFormat(string message, params object[] args)
		{
			WriteFormat(message, LogLevel.Info, args);
		}

		public void InfoEval(string message, params Func<string>[] args)
		{
			WriteEval(message, LogLevel.Info, args);
		}

		public void Warning(string message, Exception e = null)
		{
			Write(message, LogLevel.Warn, e);
		}

		public void WarningFormat(string message, params object[] args)
		{
			WriteFormat(message, LogLevel.Warn, args);
		}

		public void Error(string message, Exception e = null)
		{
			Write(message, LogLevel.Error, e);
		}

		public void Catastrophe(string message, Exception e = null)
		{
			Write(message, LogLevel.Catastrophe, e);
		}
	}
}