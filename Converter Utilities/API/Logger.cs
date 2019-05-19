using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Converter_Utilities.API {
    public class Logger {
        internal static Logger Instance(string loggerName) => new Logger(loggerName);
        private NLog.Logger logManager;

        private Logger(string loggerName) {
            LoggingConfiguration config = new LoggingConfiguration();

            FileTarget logFile = new FileTarget("logFile") { FileName = "ImageConverter.log" };
            ConsoleTarget logConsole = new ConsoleTarget("logConsole");

            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logConsole);
            config.AddRule(LogLevel.Error, LogLevel.Fatal, logFile);

            logManager = LogManager.GetLogger(loggerName);
            LogManager.Configuration = config;
        }

        internal void LogDebug(string message) => logManager.Debug(message);
        internal void LogError(string message) => logManager.Error(message);
        internal void LogError(Exception ex) => logManager.Error($"Error Message: \n {ex.Message} \n\nError Source: \n {ex.Source} \n\nError StackTrace: \n {ex.StackTrace} \n\nError TargetSite: \n {ex.TargetSite} \n\n");
    }
}
