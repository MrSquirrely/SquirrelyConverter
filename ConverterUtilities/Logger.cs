using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace ConverterUtilities {
    public class Logger {

        public static void StartLogger() {
            LoggingConfiguration config = new LoggingConfiguration();
            FileTarget logFile = new FileTarget() { FileName = "main.log", Name = "logfile" };
            ConsoleTarget logConsole = new ConsoleTarget() { Name = "logconsole" };

            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, logConsole));
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Error, logConsole));
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Error, logFile));

            LogManager.Configuration = config;
        }

        private static NLog.Logger GetLogger() => LogManager.GetLogger("Logger");

        public static void LogError(string error) => GetLogger().Error(error);
        public static void LogError(Exception ex) => GetLogger().Error($"Source: {ex.Source} {Environment.NewLine} Message: {ex.Message}");
        public static void LogDebug(string debug) => GetLogger().Debug(debug);
        public static void LogDebug(Exception ex) => GetLogger().Debug($"Source: {ex.Source} {Environment.NewLine} Message: {ex.Message}");

        public static void Dispose() => LogManager.Shutdown();
    }
}
