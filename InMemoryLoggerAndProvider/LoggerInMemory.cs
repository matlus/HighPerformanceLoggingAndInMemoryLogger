using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace InMemoryLoggerAndProvider
{
    internal sealed class LoggerInMemory : ILogger
    {
        private readonly string _categoryName;
        public List<LogEntry> LogEntries { get; } = new List<LogEntry>();

        public LoggerInMemory(string categoryName)
        {
            _categoryName = categoryName;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return (logLevel >= LogLevel.Debug);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (state != null)
            {
                var keyValuePairs = (IReadOnlyList<KeyValuePair<string, object>>)state;
                var message = formatter(state, exception);
                LogEntries.Add(new LogEntry(logLevel, eventId, keyValuePairs, message));
            }
        }
    }
}
