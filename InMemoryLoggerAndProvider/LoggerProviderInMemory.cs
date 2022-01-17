using Microsoft.Extensions.Logging;

namespace InMemoryLoggerAndProvider
{
    internal sealed class LoggerProviderInMemory : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new LoggerInMemory(categoryName);
        }

        public void Dispose()
        {
        }
    }
}
