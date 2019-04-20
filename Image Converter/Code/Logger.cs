using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Image_Converter.Code {
    internal class Logger {
        internal static Logger Instance { get; } = new Logger();
        private NLog.Logger logManager; 

        private Logger() {
            LoggingConfiguration config = new LoggingConfiguration();

            FileTarget logFile = new FileTarget("logFile") { FileName = "ImageConverter.log"};
            ConsoleTarget logConsole = new ConsoleTarget("logConsole");

            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logConsole);
            config.AddRule(LogLevel.Error, LogLevel.Fatal, logFile);

            logManager = LogManager.GetLogger("ImageLogger");
            LogManager.Configuration = config;
        }

        internal void LogDebug(string message) => logManager.Debug(message);

        internal void LogError(string message) => logManager.Error(message);
        internal void LogError(Exception ex) => logManager.Error($"Error Message: \n {ex.Message} \n\nError Source: \n {ex.Source} \n\nError StackTrace: \n {ex.StackTrace} \n\nError TargetSite: \n {ex.TargetSite} \n\n");
    }
}
