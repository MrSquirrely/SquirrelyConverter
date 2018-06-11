using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace ConverterUtilities {
    public class Logger {

        public static Logger instance = new Logger();
        private void Main() => StartLogger();

        private void StartLogger() {
            LoggingConfiguration config = new LoggingConfiguration();
            FileTarget logFile = new FileTarget() { FileName = "main.log", Name = "logfile" };
            ConsoleTarget logConsole = new ConsoleTarget() { Name = "logconsole" };

            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, logConsole));
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Error, logConsole));
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Error, logFile));

            LogManager.Configuration = config;
        }

        private NLog.Logger GetLogger() => LogManager.GetLogger("Logger");

        public void LogError(Exception ex) => GetLogger().Error($"Source: {ex.Source} {Environment.NewLine} Message: {ex.Message}");
        public void LogDebug(string debug) => GetLogger().Debug(debug);
        public void LogDebug(Exception ex) => GetLogger().Debug($"Source: {ex.Source} {Environment.NewLine} Message: {ex.Message}");

        public void Dispose() => LogManager.Shutdown();
    }
}
