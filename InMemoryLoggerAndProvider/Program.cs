using System;

namespace InMemoryLoggerAndProvider
{
    internal static partial class Program
    {
        private static readonly BlogPost blogPost = new("Azure Startup Tasks – Running as Administrator", "This is the entire Content", new DateTime(2012, 5, 12), "Shiv Kumar", new[] { "Azure", ".NET Framework" }, new[] { "Startup Tasks", "Cloud Services" });
        private static ApplicationLogger? applicationLogger;
        private static void Main(string[] args)
        {
            var executionStep = ExecutionStep.Entered;

            var configurationProvider = new ConfigurationProvider();
            var loggerProvider = new LoggerProvider("YouTube.Logs", configurationProvider.GetLoggingConfiguration, configurationProvider.GetAppInsightsInstrumentationKey());
            applicationLogger = new ApplicationLogger(loggerProvider.CreateLogger());

            executionStep = ExecutionStep.LoggerCreated;

            try
            {
                executionStep = ExecutionStep.Step1;
                ////throw new IntentionalException("This is the Exception Message");                
                applicationLogger.LogDebug(LogEvent.SomeAppEvent1, nameof(Main), executionStep, 40);
                InsertBlogPost(blogPost);

                ////executionStep = ExecutionStep.Step2;
                ////applicationLogger.LogInformation(LogEvent.SomeAppEvent2, nameof(Main), executionStep, 41);

                ////executionStep = ExecutionStep.Step3;
                ////applicationLogger.LogWarning(LogEvent.SomeAppEvent3, nameof(Main), executionStep, 42);

                ////executionStep = ExecutionStep.Completed;
            }
            catch (Exception e)
            {
                applicationLogger.LogError(4, e, executionStep);
            }
            finally
            {
                loggerProvider.Dispose();
            }
        }

        private static void InsertBlogPost(BlogPost blogPost)
        {
            //// Do the Actual Insert Here
            applicationLogger?.LogDebug(LogEvent.SomeAppEvent1, nameof(InsertBlogPost), blogPost);
        }
    }
}
