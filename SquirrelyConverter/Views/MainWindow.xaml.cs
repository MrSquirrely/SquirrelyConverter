using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using ConverterUtilities;
using Mr_Squirrely_Converters.Class;

namespace Mr_Squirrely_Converters.Views {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
            Logger.StartLogger();
            Logger.LogDebug("Logger Started");
            Toast.CreateNotifier();
            Logger.LogDebug("Toast Notifier Created");
            
            Utilities.ConverterTabs = ConverterTabs;
            CUtilities.MainWindow = this;
            CUtilities.Dispatcher = Application.Current.Dispatcher;
            CUtilities.SetWorkdingDir(Directory.GetCurrentDirectory());
            
            Options.SetGeneralSettings();


            Logger.LogDebug($"{CUtilities.GetWorkdingDir()}");

            if (File.Exists($"{CUtilities.GetWorkdingDir()}\\ImageConverter.dll")) {
                Utilities.AddImageTab();
                Options.SetImageSettings();
                CUtilities.IsImageLoaded = true;
            }
            
            if (File.Exists($"{CUtilities.GetWorkdingDir()}\\VideoConverter.dll")) {
                Utilities.AddVideoTab();
                Options.SetVideoSettings();
                CUtilities.IsVideoLoaded = true;
            }

            //CUtilities.CheckVersion(true,true,true); Todo do this better

        }

        private SettingsWindow _settingsWindow;

        private void RightWindowSettings_Click(object sender, RoutedEventArgs e) {
            _settingsWindow = new SettingsWindow { Content = new SettingsPage() };
            _settingsWindow.Show();
        }

        private void RightWindowGithub_Click(object sender, RoutedEventArgs e) => CUtilities.OpenGithub();
        private void MetroWindow_Closed(object sender, EventArgs e) => CUtilities.Dispose();

        private void ConverterTabs_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            TabItem tabItem = Utilities.ConverterTabs.SelectedItem as TabItem;
            Title = $"Mr. Squirrely's {tabItem?.Header ?? "Converters"}";
        }
    }
}
