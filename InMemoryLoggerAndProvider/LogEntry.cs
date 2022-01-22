using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace InMemoryLoggerAndProvider
{
    internal sealed record LogEntry(LogLevel LogLevel, EventId EventId, IReadOnlyList<KeyValuePair<string, object>> KeyValuePairs, string Message, Exception? exception);
}
