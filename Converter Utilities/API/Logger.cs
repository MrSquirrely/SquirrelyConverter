using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Converter_Utilities.API {
    public class Logger {
        public static Logger Instance(string loggerName) => new Logger(loggerName);
        private readonly NLog.Logger _logManager;

        private Logger(string loggerName) {
            LoggingConfiguration config = new LoggingConfiguration();

            FileTarget logFile = new FileTarget("logFile") { FileName = "ImageConverter.log" };
            ConsoleTarget logConsole = new ConsoleTarget("logConsole");

            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logConsole);
            config.AddRule(LogLevel.Error, LogLevel.Fatal, logFile);

            _logManager = LogManager.GetLogger(loggerName);
            LogManager.Configuration = config;
        }

        public void LogDebug(string message) => _logManager.Debug(message);
        public void LogError(string message) => _logManager.Error(message);
        public void LogError(Exception ex) => _logManager.Error($"Error Message: \n {ex.Message} \n\nError Source: \n {ex.Source} \n\nError StackTrace: \n {ex.StackTrace} \n\nError TargetSite: \n {ex.TargetSite} \n\n");
    }
}
