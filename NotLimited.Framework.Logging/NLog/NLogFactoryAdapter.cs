using Common.Logging;
using Common.Logging.Factory;
using Common.Logging.NLog;
using NLog;

namespace NotLimited.Framework.Logging.NLog
{
    public class NLogFactoryAdapter : AbstractCachingLoggerFactoryAdapter
    {
        private readonly LogFactory _logFactory;

        public NLogFactoryAdapter(LogFactory logFactory)
        {
            _logFactory = logFactory;
        }

        protected override ILog CreateLogger(string name)
        {
            return new NLogLoggerPublic(_logFactory.GetLogger(name));
        }
    }

    public class NLogLoggerPublic : NLogLogger
    {
        public NLogLoggerPublic(global::NLog.Logger logger) : base(logger)
        {
        }
    }
}