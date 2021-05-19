using Microsoft.Extensions.Logging;

namespace Ron.Ido.Common.Logging
{
    public class FileLoggerOptions
    {
        public const string ConfigKey = "FileLogger";
        public string FolderPath { get; set; }
        public LogLevel LogLevel { get; set; }
    }
}
