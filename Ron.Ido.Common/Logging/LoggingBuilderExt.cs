using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Ron.Ido.Common.Logging
{
    public static class LoggerFactoryExt
    {
        public static ILoggingBuilder AddFileLogging(this ILoggingBuilder builder, FileLoggerOptions options)
        {
            builder.AddProvider(new FileLoggerProvider(options));
            return builder;
        }

        public static ILoggingBuilder AddFileLogging(this ILoggingBuilder builder, IConfiguration configuration)
        {
            var section = configuration.GetSection(FileLoggerOptions.ConfigKey);
            var options = section?.Get<FileLoggerOptions>() ?? new FileLoggerOptions { FolderPath = "Logs", LogLevel = LogLevel.Trace };

            builder.AddProvider(new FileLoggerProvider(options));
            return builder;
        }
    }
}
