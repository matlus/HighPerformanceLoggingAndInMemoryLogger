using System;
using System.Collections;
using System.Collections.Generic;

namespace InMemoryLoggerAndProvider
{
    internal sealed partial class ApplicationLogger
    {
        private readonly struct BlogLogState : IReadOnlyList<KeyValuePair<string, object?>>
        {
            private readonly List<KeyValuePair<string, object?>> _keyValuePairs;
            private string MethodName { get; }
            private readonly BlogPost BlogPost { get; }            

            public BlogLogState(LogEvent logEvent, string methodName, BlogPost blogPost)
            {
                MethodName = methodName;
                BlogPost = blogPost;

                _keyValuePairs = new List<KeyValuePair<string, object?>>
                {
                    { new KeyValuePair<string, object?>("EventId", (int)logEvent) },
                    { new KeyValuePair<string, object?>("EventName", logEvent.ToString()) },
                    { new KeyValuePair<string, object?>("MethodName", methodName) },
                    { new KeyValuePair<string, object?>("BlogPost.Title", blogPost.Title) },
                    { new KeyValuePair<string, object?>("BlogPost.Content", blogPost.Content) },
                    { new KeyValuePair<string, object?>("BlogPost.Date", blogPost.Date) },
                    { new KeyValuePair<string, object?>("BlogPost.Categories", string.Join(',', blogPost.Categories))},
                    { new KeyValuePair<string, object?>("BlogPost.Tags", string.Join(',', blogPost.Tags))},
                };
            }

            public KeyValuePair<string, object?> this[int index]
            {
                get => index switch
                {
                    0 => _keyValuePairs[0],
                    1 => _keyValuePairs[1],
                    2 => _keyValuePairs[2],
                    3 => _keyValuePairs[3],
                    4 => _keyValuePairs[4],
                    5 => _keyValuePairs[5],
                    6 => _keyValuePairs[6],
                    7 => _keyValuePairs[7],

                    _ => throw new IndexOutOfRangeException(nameof(index)),
                };
            }

            public int Count => 8;

            public IEnumerator<KeyValuePair<string, object?>> GetEnumerator()
            {
                for (int i = 0; i < Count; i++)
                {
                    yield return this[i];
                }
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public override string ToString()
            {
                return $"Debug Log from Method: `{MethodName}`, BlopPost Data: Title: `{BlogPost.Title}`, Content: `{BlogPost.Content}` Date: `{BlogPost.Date}`, Categories: `{string.Join(',', BlogPost.Categories)}`, Tags: `{string.Join(',', BlogPost.Tags)}`";
            }

            public static string Format(BlogLogState state, Exception? exception) => state.ToString();
        }
    }
}
