using Converters.Lib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Controls;

namespace Converters.Win {
    internal class Config {

        internal static List<IConverter> Converters = new List<IConverter>();
        internal static string PluginDir = $"{AppContext.BaseDirectory}/Plugins";

        internal static List<Page> MainViews = new List<Page>();
        internal static List<Page> SettingsViews = new List<Page>();
        internal static List<Page> AboutViews = new List<Page>();

        internal static void FindPlugins() {
            foreach (string file in Directory.EnumerateFiles(PluginDir, "*.dll", SearchOption.AllDirectories)) {
                if (File.Exists(file)) {
                    Assembly assembly = Assembly.Load(file);
                    foreach (Type type in assembly.GetTypes()) {

                    }
                    //loaders.Add(PluginLoader.CreateFromAssemblyFile(file, sharedTypes: new[] { typeof(IConverter) } ));
                }
            }
        }

        internal static void LoadPlugins() {

            foreach (string file in Directory.EnumerateFiles(PluginDir, "*.dll", SearchOption.AllDirectories)) {
                try {
                    Assembly assembly = Assembly.LoadFrom(file);
                    foreach (Type pluginType in assembly.GetTypes().Where(t => typeof(IConverter).IsAssignableFrom(t) && !t.IsAbstract)) {
                        Converters.Add(Activator.CreateInstance(pluginType) as IConverter);

                    }
                }
                catch (Exception ex) {
                    Debug.WriteLine(ex);
                }
                
            }

            //foreach (PluginLoader loader in loaders) {
            //    foreach (Type pluginType in loader.LoadDefaultAssembly().GetTypes().Where(t => typeof(IConverter).IsAssignableFrom(t) && !t.IsAbstract)) {
            //        Converters.Add(Activator.CreateInstance(pluginType) as IConverter);
            //        //MainViews.Add(converter.GetMainView());
            //        //SettingsViews.Add(converter.GetSettingsView());
            //        //AboutViews.Add(converter.GetAboutView());
            //    }
            //}
        }


    }
}
