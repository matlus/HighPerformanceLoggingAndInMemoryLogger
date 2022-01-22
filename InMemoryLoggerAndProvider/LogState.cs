using System;
using System.Collections;
using System.Collections.Generic;

namespace InMemoryLoggerAndProvider
{
    internal sealed class BlogPostLogState : LogStateBase<BlogPost>
    {
        private readonly BlogPost blogPost;

        public BlogPostLogState(LogEvent logEvent, string methodName, BlogPost blogPost)
            : base(logEvent, methodName, blogPost)
        {
            this.blogPost = blogPost;
        }

        protected override string ToLogMessage()
        {
            return $"Debug Log from Method: `{MethodName}`, BlopPost Data: Title: `{blogPost.Title}`, Content: `{blogPost.Content}` Date: `{blogPost.Date}`, Categories: `{string.Join(',', blogPost.Categories)}`, Tags: `{string.Join(',', blogPost.Tags)}`";
        }
    }

    internal abstract class LogStateBase<T> : IReadOnlyList<KeyValuePair<string, object?>> where T : struct
    {
        private readonly List<KeyValuePair<string, object?>> _keyValuePairs;

        protected string MethodName { get; }

        static LogStateBase()
        {

        }

        public LogStateBase(LogEvent logEvent, string methodName, T dto)
        {
            MethodName = methodName;

            _keyValuePairs = new List<KeyValuePair<string, object?>>
                {
                    { new KeyValuePair<string, object?>("EventId", (int)logEvent) },
                    { new KeyValuePair<string, object?>("EventName", logEvent.ToString()) },
                    { new KeyValuePair<string, object?>("MethodName", methodName) },
                };

            var propertyInfos = dto.GetType().GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                var key = propertyInfo.Name;
                var value = propertyInfo.GetValue(dto, null);
                _keyValuePairs.Add(new KeyValuePair<string, object?>(key, value));
            }
        }

        public KeyValuePair<string, object?> this[int index]
        {
            get => _keyValuePairs[index];
        }

        public int Count => _keyValuePairs.Count;

        public IEnumerator<KeyValuePair<string, object?>> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        protected abstract string ToLogMessage();

        public string Format(LogStateBase<T> state, Exception? exception) => state.ToLogMessage();
    }
}
