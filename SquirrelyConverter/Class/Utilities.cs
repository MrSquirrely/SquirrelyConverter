using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Controls;
using ConverterUtilities;
using ConverterUtilities.Properties;
using Dragablz;
using Humanizer;

namespace Mr_Squirrely_Converters.Class {
    internal static class Utilities {

        private static int _converterTabs;
        private static int _settingsTabs;
        public static TabablzControl ConverterTabs { get; set; }
        public static TabablzControl SettingsTabs { get; set; }
        private static readonly List<Assembly> Assemblies = new List<Assembly>();

        public static void LoadAssemblies() {
            foreach (string assembly in Directory.GetFiles($"{Directory.GetCurrentDirectory()}\\Converters", "*.dll")) {
                Assemblies.Add(Assembly.LoadFile(assembly));
            }
        }

        public static void AddViews() {

            foreach (Assembly assembly in Assemblies) {
                string name = assembly.GetName().Name;
                Type startupType = assembly.GetType($"{name}.View.MainView");
                object activator = Activator.CreateInstance(startupType);
                AddTab(activator, name);
            }
        }

        public static void AddSettingViews() {

            foreach (Assembly assembly in Assemblies) {
                string name = assembly.GetName().Name;
                Type settingsType = assembly.GetType($"{name}.View.SettingView");
                object settingsActivator = Activator.CreateInstance(settingsType);
                AddSettingsTab(settingsActivator, name);
            }
        }

        private static void AddTab(object view, string header) {
            _converterTabs = ConverterTabs.Items.Count;

            Logger.LogDebug("Adding View To Converter Tabs");
            TabItem tab = new TabItem() {
                Content = view,
                Header = header.Humanize()
            };
            ConverterTabs.Items.Insert(_converterTabs, tab);
        }

        private static void AddSettingsTab(object view, string header) {
            _settingsTabs = SettingsTabs.Items.Count;

            Logger.LogDebug("Adding View To Settings Tabs");
            TabItem tab = new TabItem() {
                Content = view,
                Header = header.Humanize()
            };
            SettingsTabs.Items.Insert(_settingsTabs, tab);
        }
    }
}
