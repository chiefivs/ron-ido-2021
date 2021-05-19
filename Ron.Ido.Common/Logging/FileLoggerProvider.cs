using Microsoft.Extensions.Logging;

namespace Ron.Ido.Common.Logging
{
    [ProviderAlias("FileLogger")]
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly FileLoggerOptions _options;

        public FileLoggerProvider(FileLoggerOptions options)
        {
            _options = options;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(_options);
        }

        public void Dispose()
        {
        }
    }
}
