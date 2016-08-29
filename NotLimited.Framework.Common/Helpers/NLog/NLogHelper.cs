using System.Reflection;
using System.Text;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace NotLimited.Framework.Common.Helpers.NLog
{
	public static class NLogHelper
	{
		public static void ConfigureConsoleAndFileLogger()
		{
			LogManager.Configuration = GetConsoleAndFileLoggingConfig();
		}

		public static LogFactory GetConsoleAndFileLoggingFactory()
		{
			return new LogFactory(GetConsoleAndFileLoggingConfig());
		}

		private static LoggingConfiguration GetConsoleAndFileLoggingConfig()
		{
			var config = new LoggingConfiguration();

			config.AddTarget("console", new ColoredConsoleTarget { Layout = new SimpleLayout("${logger}: ${message} ${exception:format=tostring}") });
			config.AddTarget("file", new FileTarget
			{
				FileName = new SimpleLayout("${basedir}/logs/" + Assembly.GetCallingAssembly().GetName().Name + ".txt"),
				Layout = new SimpleLayout("${longdate} ${logger}: ${message} ${exception:format=tostring}"),
				ArchiveFileName = new SimpleLayout("${basedir}/archives/log.{#####}.txt"),
				ArchiveAboveSize = 1024000,
				ArchiveNumbering = ArchiveNumberingMode.Sequence,
				ConcurrentWrites = true,
				KeepFileOpen = false,
				Encoding = Encoding.UTF8,
				MaxArchiveFiles = 1,
				EnableFileDelete = true
			});

			config.AddRule(LogLevel.Debug, LogLevel.Fatal, "console");
			config.AddRule(LogLevel.Debug, LogLevel.Fatal, "file");

			return config;
		}
	}
}