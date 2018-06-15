using System;
using System.Windows;
using System.Windows.Controls;
using Mr_Squirrely_Converters.Class;
using System.IO;
using ConverterUtilities;

namespace Mr_Squirrely_Converters.Views {
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page {
        public MainPage() {
            InitializeComponent();
            Utilities.ConverterTabs = ConverterTabs;
            CUtilities.CheckVersion(true, true, true);
            CUtilities.SetWorkdingDir(Directory.GetCurrentDirectory());
            try {
                if (File.Exists($"{CUtilities.GetWorkdingDir()}ImageConverter.dll")) {
                    Utilities.AddImageTab();
                }
            }
            catch {
                Logger.LogDebug("ImageConverter.dll wasn't found");
            }


            try {
                if (File.Exists($"{CUtilities.GetWorkdingDir()}VideoConverter.dll")) {
                    Utilities.AddVideoTab();
                }
            }
            catch {
                Logger.LogDebug("VideoConverter.dll wasn't found");

            }
            




        }
    }
}
