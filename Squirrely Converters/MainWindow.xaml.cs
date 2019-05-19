using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Converter_Utilities;
using Converter_Utilities.Interface;

namespace Squirrely_Converters {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow  {

        Type pluginType = typeof(IConverter);
        string[] converterFileNames;
        ICollection<Assembly> assemblies;
        ICollection<Type> pluginTypes = new List<Type>();
        List<IConverter> converters;

        public MainWindow() {
            InitializeComponent();
            try {
                converterFileNames = Directory.GetFiles(Environment.CurrentDirectory, "*.converter");
                assemblies = new List<Assembly>(converterFileNames.Length);
                foreach (string converter in converterFileNames) {
                    AssemblyName name = AssemblyName.GetAssemblyName(converter);
                    Assembly assembly = Assembly.Load(name);
                    assemblies.Add(assembly);
                }

                foreach (Assembly assembly1 in assemblies) {
                    if (assembly1 != null) {
                        Type[] types = assembly1.GetTypes();
                        foreach (Type type in types) {
                            if (type.IsInterface || type.IsAbstract) {
                                continue;
                            }
                            else {
                                if (type.GetInterface(pluginType.FullName) != null) {
                                    pluginTypes.Add(type);
                                }
                            }
                        }
                    }
                }

                converters = new List<IConverter>(pluginTypes.Count);
                foreach (Type type1 in pluginTypes) {
                    IConverter converter = (IConverter)Activator.CreateInstance(type1);
                    converters.Add(converter);
                }
            }
            catch (Exception ex) {
                //Todo: Logging
            }
            Content = converters[0].MainPage;
        }
    }
}
