using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mr_Squirrely_Converters.Class {
    static class Logger {

        public static void StartLogger() {
            LoggingConfiguration config = new LoggingConfiguration();
            FileTarget logFile = new FileTarget() { FileName = "log.txt", Name = "logfile"};
            ConsoleTarget logConsole = new ConsoleTarget() { Name = "logconsole" };

            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, logConsole));
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Error, logConsole));
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Error, logFile));

            LogManager.Configuration = config;
        }

        public static NLog.Logger GetLogger() => LogManager.GetLogger("Logger");

        public static void LogError(string error) => GetLogger().Error(error);
        public static void LogDebug(string debug) => GetLogger().Debug(debug);

        public static void Shutdown() => LogManager.Shutdown();
    }
}
