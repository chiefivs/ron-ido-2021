using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;

namespace Ron.Ido.Common.Logging
{
    public class FileLogger : ILogger
    {

        public FileLogger(FileLoggerOptions options)
        {
            _folderPath = options.FolderPath;
            _logLevel = options.LogLevel;
        }

        private readonly string _folderPath;
        private LogLevel _logLevel;

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _logLevel <= logLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            try
            {
                var item = state as LogItem;
                if (item == null)
                    item = new LogItem() { Message = state.ToString() };

                if (!Guid.TryParse(eventId.Name, out Guid res))
                    eventId = new EventId(eventId.Id, string.IsNullOrEmpty(eventId.Name) ? Guid.NewGuid().ToString() : $"{eventId.Name}_{Guid.NewGuid()}");

                item.Exception = new ExceptionInfo(exception);
                var json = item.ToJson();

                File.WriteAllText(Path.Combine(_folderPath, $"{logLevel}_{eventId.Name}.log"), json);
            }
            catch
            {
                
            }
        }
    }
}
