using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;

namespace Ron.Ido.Common.Logging
{
    public static class LoggerExt
    {
        public static void Log(this ILogger logger, LogLevel level, Guid logid, LogItem log, Exception exception = null)
        {
            logger.Log(level, new EventId(0, logid.ToString()), log, exception, LogFormatter);
        }

        public static void LogCritical(this ILogger logger, Guid logid, LogItem log, Exception exception = null)
        {
            Log(logger, LogLevel.Critical, logid, log, exception);
        }

        public static void LogDebug(this ILogger logger, Guid logid, LogItem log, Exception exception = null)
        {
            Log(logger, LogLevel.Debug, logid, log, exception);
        }

        public static void LogError(this ILogger logger, Guid logid, LogItem log, Exception exception = null)
        {
            Log(logger, LogLevel.Error, logid, log, exception);
        }

        public static void LogInformation(this ILogger logger, Guid logid, LogItem log, Exception exception = null)
        {
            Log(logger, LogLevel.Information, logid, log, exception);
        }

        public static void LogTrace(this ILogger logger, Guid logid, LogItem log, Exception exception = null)
        {
            Log(logger, LogLevel.Trace, logid, log, exception);
        }

        public static void LogWarning(this ILogger logger, Guid logid, LogItem log, Exception exception = null)
        {
            Log(logger, LogLevel.Warning, logid, log, exception);
        }

        private static string LogFormatter(LogItem log, Exception ex)
        {
            log.Exception = new ExceptionInfo(ex);
            var json = log.ToJson();
            return json;
        }
    }
}
