using System;

namespace InMemoryLoggerAndProvider
{
    internal sealed record BlogPost(string Title, string Content, DateTime Date, string PostedBy, string[] Categories, string[] Tags);
}
