using System.Reflection;
using System.Text;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace NotLimited.Framework.Logging.NLog
{
	public static class NLogHelper
	{
	    public static void ConfigureCommonLoggingWithNLog(LogFactory factory)
	    {
	        Common.Logging.LogManager.Adapter = new NLogFactoryAdapter(factory);
	    }

		public static void ConfigureConsoleAndFileLogger()
		{
			LogManager.Configuration = GetConsoleAndFileLoggingConfig(Assembly.GetCallingAssembly().GetName().Name);
		}

		public static LogFactory GetConsoleAndFileLoggingFactory()
		{
			return new LogFactory(GetConsoleAndFileLoggingConfig(Assembly.GetCallingAssembly().GetName().Name));
		}

		private static LoggingConfiguration GetConsoleAndFileLoggingConfig(string assemblyName)
		{
			var config = new LoggingConfiguration();

			config.AddTarget("console", new ColoredConsoleTarget { Layout = new SimpleLayout("${logger}: ${message} ${exception:format=tostring}") });
			config.AddTarget("file", new FileTarget
			{
				FileName = new SimpleLayout("${basedir}/logs/" + assemblyName + ".txt"),
				Layout = new SimpleLayout("${longdate} [${level}] ${logger}: ${message} ${exception:format=tostring}"),
				ArchiveFileName = new SimpleLayout("${basedir}/archives/log.{#####}.txt"),
				ArchiveAboveSize = 1024000,
				ArchiveNumbering = ArchiveNumberingMode.Sequence,
				ConcurrentWrites = true,
				KeepFileOpen = false,
				Encoding = Encoding.UTF8,
				MaxArchiveFiles = 1,
				EnableFileDelete = true
			});

			config.AddRule(global::NLog.LogLevel.Debug, global::NLog.LogLevel.Fatal, "console");
			config.AddRule(global::NLog.LogLevel.Debug, global::NLog.LogLevel.Fatal, "file");

			return config;
		}
	}
}