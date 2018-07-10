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

//using ImageConverter.View;
//using VideoConverter.View;

namespace Mr_Squirrely_Converters.Class {
    internal static class Utilities {

        private static int _tabs;
        public static TabablzControl ConverterTabs { get; set; }
        private static readonly List<Assembly> Assemblies = new List<Assembly>();

        public static void AddViews() {

            foreach (string assembly in Directory.GetFiles($"{Directory.GetCurrentDirectory()}\\Converters", "*.dll")) {
                Assemblies.Add(Assembly.LoadFile(assembly));
            }

            foreach (Assembly assembly in Assemblies) {
                string name = assembly.GetName().Name;
                Type startupType = assembly.GetType($"{name}.View.MainView");
                object activator = Activator.CreateInstance(startupType);
                AddTab(activator, name);
            }

        }

        private static void AddTab(object view, string header) {
            _tabs = ConverterTabs.Items.Count;

            Logger.LogDebug("Adding View");
            TabItem tab = new TabItem() {
                Content = view,
                Header = header.Humanize()
            };
            ConverterTabs.Items.Insert(_tabs, tab);
        }

        public static void AddImageTab() {
            try {
                Logger.LogDebug("Adding Image Tab");
                _tabs = ConverterTabs.Items.Count;

                const string nameOfNamespace = "ImageConverter";

                Assembly assembly = Assembly.LoadFrom($"{nameOfNamespace}.dll");
                Type imageViewType = assembly.GetType($"{nameOfNamespace}.View.MainView");
                object instanceOfImageViewType = Activator.CreateInstance(imageViewType);

                TabItem imageTab = new TabItem {
                    Content = instanceOfImageViewType,
                    Header = Resources.ImageHeader
                };
                ConverterTabs.Items.Insert(_tabs, imageTab);
            }
            catch (Exception ex) {
                Logger.LogError(ex);
            }
            
        }

        public static void AddVideoTab() {
            try {
                Logger.LogDebug("Adding Video Tab");
                _tabs = ConverterTabs.Items.Count;

                Assembly assembly = Assembly.LoadFrom("VideoConverter.dll");
                Type videoViewType = assembly.GetType("VideoConverter.View.VideoView");
                object typeOfVideoView = Activator.CreateInstance(videoViewType);
                
                TabItem videoTab = new TabItem {
                    Content = typeOfVideoView,
                    Header = Resources.VideoHeader
                };
                ConverterTabs.Items.Insert(_tabs, videoTab);
            }
            catch (Exception ex) {
                Logger.LogError(ex);
            }
            
        }
    }
}
