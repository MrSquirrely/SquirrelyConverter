using System;
using System.Diagnostics;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace ConverterUtilities.CUtils {
    public class Logger {

        private static bool Started { get; set; }

        /// <summary>
        /// Used tot start the logger
        /// </summary>
        public static void StartLogger() {
            if (!Started) {
                LoggingConfiguration config = new LoggingConfiguration();
                FileTarget logFile = new FileTarget() { FileName = "main.log", Name = "logfile" };
                ConsoleTarget logConsole = new ConsoleTarget() { Name = "logconsole" };

                config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, logConsole));
                config.LoggingRules.Add(new LoggingRule("*", LogLevel.Error, logConsole));
                config.LoggingRules.Add(new LoggingRule("*", LogLevel.Error, logFile));

                LogManager.Configuration = config;
            }
            else {
                LogDebug("The logger is already started no need to start it.");
            }
        }

        private static NLog.Logger GetLogger() => LogManager.GetLogger("Logger");

        public static void LogError(string error) => GetLogger().Error(error);
        public static void LogError(Exception ex) => GetLogger().Error($"{Environment.NewLine} Source: {ex.Source} " +
                                                                       $"{Environment.NewLine} Message: {ex.Message}" +
                                                                       $"{Environment.NewLine} TargetSite: {ex.TargetSite}" +
                                                                       $"{Environment.NewLine} StackTrace: {Environment.NewLine} {ex.StackTrace}");
        [Conditional("DEBUG")]
        public static void LogDebug(string debug) => GetLogger().Debug(debug);
        [Conditional("DEBUG")]
        public static void LogDebug(Exception ex) => GetLogger().Debug($"{Environment.NewLine} Source: {ex.Source}" +
                                                                       $"{Environment.NewLine} Message: {ex.Message}" +
                                                                       $"{Environment.NewLine} TargetSite: {ex.TargetSite}" +
                                                                       $"{Environment.NewLine} StackTrace: {Environment.NewLine} {ex.StackTrace}");

        public static void Dispose() => LogManager.Shutdown();
    }
}
