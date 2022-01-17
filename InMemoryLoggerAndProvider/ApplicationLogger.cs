using Microsoft.Extensions.Logging;
using System;

namespace InMemoryLoggerAndProvider
{
    internal sealed partial class ApplicationLogger
    {
        private readonly ILogger _logger;
        public ApplicationLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void LogError(int eventId, Exception exception, ExecutionStep executionStep)
        {
            LogExceptionCodeGen(_logger, eventId, exception, executionStep, exception.GetType().Name, exception.Message);
        }

        public void LogWarning(LogEvent logEvent, string methodName, ExecutionStep executionStep, int id)
        {
            LogWarningCodeGen(_logger, (int)logEvent, methodName, executionStep, id);
        }

        public void LogInformation(LogEvent logEvent, string methodName, ExecutionStep executionStep, int id)
        {
            LogWarningCodeGen(_logger, (int)logEvent, methodName, executionStep, id);
        }

        public void LogDebug(LogEvent logEvent, string methodName, ExecutionStep executionStep, int id)
        {
            ////LogDebugCodeGen(_logger, (int)logEvent, methodName, executionStep, id);

            if (_logger.IsEnabled(LogLevel.Debug))
            {
                var logDebugState = new LogDebugState(logEvent, methodName, executionStep, id);
                _logger.Log(LogLevel.Debug, new EventId((int)logEvent, logEvent.ToString()), logDebugState, exception: null, LogDebugState.Format);
            }
        }

        public void LogDebug(LogEvent logEvent, string methodName, BlogPost blogPost)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                var blogLogState = new BlogLogState(logEvent, methodName, blogPost);
                _logger.Log(LogLevel.Debug, new EventId((int)logEvent, logEvent.ToString()), blogLogState, exception: null, BlogLogState.Format);
            }
        }

        [LoggerMessage(
            EventId = 1,
            EventName = "CatchBlock",
            Level = LogLevel.Error,
            SkipEnabledCheck = false,
            Message = "An Exception occured during the processing of Blah. In ExecutionStep: {ExecutionStep}. Exception Type: {ExceptionType} with Message: {ExceptionMessage}")]
        public static partial void LogExceptionCodeGen(ILogger logger, int eventId, Exception exception, ExecutionStep executionStep, string exceptionType, string exceptionMessage);

        [LoggerMessage(
            Level = LogLevel.Warning,
            SkipEnabledCheck = false,
            Message = "Warning in Method: {MethodName} and ExecutionStep: {ExecutionStep}. Getting item {Id}")]
        public static partial void LogWarningCodeGen(ILogger logger, int eventId, string methodName, ExecutionStep executionStep, int id);

        [LoggerMessage(
            Level = LogLevel.Information,
            SkipEnabledCheck = false,
            Message = "Information from Method: {MethodName} and ExecutionStep: {ExecutionStep}. Getting item {Id}")]
        public static partial void LogInformationCodeGen(ILogger logger, int eventId, string methodName, ExecutionStep executionStep, int id);

        [LoggerMessage(
            Level = LogLevel.Debug,
            SkipEnabledCheck = false,
            Message = "Debug Log from Method: {MethodName} and ExecutionStep: {ExecutionStep}. Getting item {Id}")]
        public static partial void LogDebugCodeGen(ILogger logger, int eventId, string methodName, ExecutionStep executionStep, int id);
    }
}
