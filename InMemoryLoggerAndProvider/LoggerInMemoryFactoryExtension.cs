using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace InMemoryLoggerAndProvider
{
    internal static class LoggerInMemoryFactoryExtensions
    {
        public static ILoggingBuilder AddInMemoryLogger(this ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, LoggerProviderInMemory>());
            return loggingBuilder;
        }
    }
}
