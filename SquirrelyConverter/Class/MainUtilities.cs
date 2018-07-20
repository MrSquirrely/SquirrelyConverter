using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Controls;
using ConverterUtilities;
using ConverterUtilities.Configs;
using ConverterUtilities.CUtils;
using ConverterUtilities.Properties;
using Dragablz;
using Humanizer;
using Newtonsoft.Json;

namespace Mr_Squirrely_Converters.Class {

    internal static class MainUtilities {
        private static int _converterTabs;
        private static int _settingsTabs;
        public static TabablzControl ConverterTabs { get; set; }
        public static TabablzControl SettingsTabs { get; set; }
        private static readonly List<Assembly> Assemblies = new List<Assembly>();
        private static List<ConverterInfo> converters = new List<ConverterInfo>();

        public static void LoadAssemblies() {

            foreach (string directory in Directory.GetDirectories($"{Directory.GetCurrentDirectory()}\\Converters")) {
                string[] converter = Directory.GetFiles(directory, "*.converter");
                StreamReader reader = new StreamReader($"{converter[0]}");
                JsonSerializer serializer = new JsonSerializer();
                converters.Add((ConverterInfo) serializer.Deserialize(reader, typeof(ConverterInfo)));
            }

            Utilities.SetConverterInfos(converters);
            AddViews();
        }

        public static void AddViews() {
            try {
                foreach (ConverterInfo converterInfo in Utilities.GetConverterInfos()) {
                    AssemblyName assemblyName = AssemblyName.GetAssemblyName($"{DirectoryInfos.WorkingDirectory}\\Converters\\{converterInfo.ConverterName}\\{converterInfo.AssemblyName}.dll");
                    Assembly assembly = Assembly.Load(assemblyName);
                    Type startType = assembly.GetType(converterInfo.ConverterView);
                    //Type settingsType = assembly.GetType(converterInfo.SettingsView);
                    object startInstance = Activator.CreateInstance(startType);
                    //object settingsInstance = Activator.CreateInstance(settingsType);
                    AddTab(startInstance, converterInfo.ConverterName);
                    //AddSettingsTab(settingsInstance, converterInfo.ConverterName);
                }
            }
            catch (Exception ex) {
                Logger.LogError(ex);
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
