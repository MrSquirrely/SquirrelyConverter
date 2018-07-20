using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using ConverterUtilities.Configs;
using MahApps.Metro.Controls;

namespace ConverterUtilities.CUtils {
    public class Utilities {

        private static List<ConverterInfo> ConverterInfos;
        private static int Threads{ get; set; }
        private static bool Converting { get; set; }
        private static MetroWindow MainWindow { get; set; }
        private static Window SettingsWindow { get; set; }
        private static Dispatcher Dispatcher { get; set; }
        private static ConverterList List { get; set; }

        public static void Startup(MetroWindow mainWindow, Dispatcher dispatcher, string workingDir) {
            Logger.StartLogger();
            Logger.LogDebug("Logger Started");

            Toast.CreateNotifier();
            Logger.LogDebug("Toast Notifier Created");

            SetMainWindow(mainWindow);
            Logger.LogDebug($"Main Window set to {mainWindow}");

            SetDispatcher(dispatcher);
            Logger.LogDebug($"Dispatcher set to {dispatcher}");

            DirectoryInfos.WorkingDirectory = workingDir;
            Logger.LogDebug($"Working Directoory set to {workingDir}");
        }
    
        public static List<ConverterInfo> GetConverterInfos() => ConverterInfos;
        public static void ClearConverterInfos() => ConverterInfos.Clear();
        public static void SetConverterInfos(List<ConverterInfo> value) => ConverterInfos = value;

        public static int GetThreads() => Threads;
        public static void AddThread() => Threads++;
        public static void RemoveThread() => Threads--;

        public static bool IsConverting() => Converting;
        public static void SetConverting(bool value) => Converting = value;

        public static MetroWindow GetMainWindow() => MainWindow;
        public static void SetMainWindow(MetroWindow value) => MainWindow = value;

        public static Window GetSettingsWindow() => SettingsWindow;
        public static void SetSettingsWindow(Window value) => SettingsWindow = value;

        public static Dispatcher GetDispatcher() => Dispatcher;
        public static void SetDispatcher(Dispatcher value) => Dispatcher = value;

        public static ConverterList GetList() => List;
        public static void SetList(ConverterList value) => List = value;

        public static void Dispose() {
            try {
                Toast.Dispose();
            }
            catch (Exception ex) {
                Logger.LogError(ex);
            }

            try {
                if (SettingsWindow.IsLoaded) {
                    SettingsWindow.Close();
                }
            }
            catch (Exception ex) {
                Logger.LogError(ex);
            }

            try {
                Logger.Dispose();
            }
            catch (Exception ex) {
                Logger.LogError(ex);
            }
            
        }

    }
}
