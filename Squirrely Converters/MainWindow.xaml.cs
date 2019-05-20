using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Policy;
using System.Windows;
using Converter_Utilities.API;
using Converter_Utilities.Interface;

namespace Squirrely_Converters {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow  {
        readonly Type _pluginType = typeof(IConverter);
        readonly ICollection<Type> _pluginTypes = new List<Type>();
        readonly List<IConverter> _converters;

        public MainWindow() {
            InitializeComponent();
            try {
                string[] converterFileNames = Directory.GetFiles(Environment.CurrentDirectory, "*.converter");
                ICollection<Assembly> assemblies = new List<Assembly>(converterFileNames.Length);
                foreach (string converter in converterFileNames) {
                    AssemblyName name = AssemblyName.GetAssemblyName(converter);
                    Assembly assembly = Assembly.Load(name);
                    assemblies.Add(assembly);
                }

                foreach (Assembly assembly1 in assemblies) {
                    if (assembly1 == null) continue;
                    Type[] types = assembly1.GetTypes();
                    foreach (Type type in types) {
                        if (type.IsInterface && type.IsAbstract) continue;
                        if (type.GetInterface(_pluginType.FullName) != null) {
                            _pluginTypes.Add(type);
                        }
                    }
                }

                _converters = new List<IConverter>(_pluginTypes.Count);
                foreach (Type type1 in _pluginTypes) {
                    IConverter converter = (IConverter)Activator.CreateInstance(type1);
                    _converters.Add(converter);
                }
            }
            catch (Exception ex) {
                Logger.Instance("ConverterViewer").LogError(ex);
            }
            Content = _converters[0].MainPage;
        }
    }
}
