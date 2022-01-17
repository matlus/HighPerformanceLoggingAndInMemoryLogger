using System;
using System.Collections;
using System.Collections.Generic;

namespace InMemoryLoggerAndProvider
{
    internal sealed partial class ApplicationLogger
    {
        private readonly struct LogDebugState : IReadOnlyList<KeyValuePair<string, object?>>
        {
            private readonly int Id { get; }
            private readonly string MethodName { get; }
            private readonly ExecutionStep ExecutionStep { get; }
            private readonly List<KeyValuePair<string, object?>> _keyValuePairs;

            public LogDebugState(LogEvent logEvent, string methodName, ExecutionStep executionStep, int id)
            {
                MethodName = methodName;
                Id = id;
                ExecutionStep = executionStep;

                _keyValuePairs = new List<KeyValuePair<string, object?>>
                {
                    { new KeyValuePair<string, object?>("EventId", (int)logEvent) },
                    { new KeyValuePair<string, object?>("EventName", logEvent.ToString()) },
                    { new KeyValuePair<string, object?>("MethodName", methodName) },
                    { new KeyValuePair<string, object?>("ExecutionStep", executionStep) },
                    { new KeyValuePair<string, object?>("Id", id) },
                    { new KeyValuePair<string, object?>("{OriginalFormat}", "Debug Log in Method: {MethodName} and ExecutionStep: {ExecutionStep}. Getting item {Id}") }
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

                    _ => throw new IndexOutOfRangeException(nameof(index)),
                };
            }

            public int Count => 6;

            public IEnumerator<KeyValuePair<string, object?>> GetEnumerator()
            {
                for (int i = 0; i < Count; i++)
                {
                    yield return this[i];
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public override string ToString()
            {
                return $"Debug Log from Method: `{MethodName}` and ExecutionStep: `{ExecutionStep}`. Getting item `{Id}`";
            }

            public static string Format(LogDebugState state, Exception? exception) => state.ToString();
        }
    }
}
